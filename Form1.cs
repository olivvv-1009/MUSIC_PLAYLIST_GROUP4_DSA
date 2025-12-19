using AxWMPLib;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace CTDL
{
    public partial class Form1 : Form
    {
        private Playlist playlist = new Playlist();
        private bool isShuffle = false;
        private bool isUserAction = false;
        private bool isPausedByStop = false;
        private Timer marqueeTimer = new Timer();
        private string marqueeText = "";
        private bool isPlaying = false;
        private Timer timerProgress = new Timer();
        private Label lblTime;

        public Form1()
        {
            InitializeComponent();

            RoundPanel(panelPlaylist, 15);

            lblPlaylist.Text = "My Playlist";
            lblPlaylist.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblPlaylist.TextAlign = ContentAlignment.MiddleCenter;

            txtSearch.Text = "Search song...";
            txtSearch.ForeColor = Color.Gray;
            txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.Text == "Search song...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };
            txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Search song...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;

            btnAddSong.Click += BtnAddSong_Click;
            btnRemoveSong.Click += BtnRemoveSong_Click;
            btnSortByName.Click += BtnSortByName_Click;
            btnShuffle.Click += BtnShuffle_Click;
            btnReverse.Click += BtnReverse_Click;
            btnPlay.Click += BtnPlay_Click;
            btnStop.Click += BtnStop_Click;
            btnPrevious.Click += BtnPrevious_Click;
            btnNext.Click += BtnNext_Click;
            lstSongs.DoubleClick += LstSongs_DoubleClick;

            axWindowsMediaPlayer.PlayStateChange += AxWindowsMediaPlayer_PlayStateChange;

            SetAlbumIdle();
        }

        private void RoundPanel(Panel panel, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            panel.Region = new Region(path);
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            lstSongs.Items.Clear();
            foreach (var name in playlist.GetAllNames())
                if (name.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    lstSongs.Items.Add(name);
        }

        private void BtnAddSong_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "MP3 Files|*.mp3", Multiselect = true };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            foreach (string file in dlg.FileNames)
                playlist.AddSong(file, Path.GetFileName(file));

            RefreshList();
            UpdateSongCount();
        }

        private void BtnRemoveSong_Click(object sender, EventArgs e)
        {
            if (lstSongs.SelectedIndex < 0) return;
            playlist.RemoveAt(lstSongs.SelectedIndex);
            RefreshList();
            UpdateSongCount();
        }

        private void BtnSortByName_Click(object sender, EventArgs e)
        {
            playlist.InsertionSort();
            RefreshList();
        }

        private void BtnShuffle_Click(object sender, EventArgs e)
        {
            isShuffle = !isShuffle;
            playlist.Shuffle();
            RefreshList();
        }

        private void BtnReverse_Click(object sender, EventArgs e)
        {
            playlist.Reverse();
            RefreshList();
        }

        private void LstSongs_DoubleClick(object sender, EventArgs e)
        {
            if (lstSongs.SelectedIndex == -1) return;

            SongNode song = playlist.PlayAt(lstSongs.SelectedIndex);
            PlaySong(song);
        }


        private void BtnPlay_Click(object sender, EventArgs e)
        {
            // Nếu đang pause → phát tiếp
            if (axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                axWindowsMediaPlayer.Ctlcontrols.play();
                SetAlbumPlaying();
                StartMarquee(); // resume marquee
                btnPlay.Text = "▶";
                isPlaying = true;
                return;
            }

            // Nếu đang play → KHÔNG làm gì
            if (axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                return;
            }

            // Nếu chưa có bài nào → phát bài đầu
            if (playlist.Current == null && playlist.Count > 0)
            {
                SongNode song = playlist.PlayAt(0);
                PlaySong(song);   // gọi PlaySong để set URL
                btnPlay.Text = "▶";
                isPlaying = true;
                StartMarquee();   // đảm bảo chữ chạy
                return;
            }

            // Nếu đang chọn 1 bài trong list → phát bài đó
            if (lstSongs.SelectedIndex >= 0 && playlist.Current == null)
            {
                SongNode song = playlist.PlayAt(lstSongs.SelectedIndex);
                PlaySong(song);
                btnPlay.Text = "▶";
                isPlaying = true;
                StartMarquee();
                return;
            }

            // Nếu đã có bài đang phát → play
            if (!isPlaying)
            {
                axWindowsMediaPlayer.Ctlcontrols.play();
                isPlaying = true;
                btnPlay.Text = "▶";
                StartMarquee();
            }
            else
            {
                axWindowsMediaPlayer.Ctlcontrols.pause();
                isPlaying = false;
                btnPlay.Text = "▶";
                marqueeTimer.Stop();
            }
        }


        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer.Ctlcontrols.pause();
                SetAlbumIdle();
            }
            axWindowsMediaPlayer.Ctlcontrols.pause(); // KHÔNG stop
            isPlaying = false;
            btnPlay.Text = "▶";
            marqueeTimer.Stop();
        }



        private void BtnNext_Click(object sender, EventArgs e)
        {
            PlaySong(playlist.Next());
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            PlaySong(playlist.Previous());
        }

        private void PlaySong(SongNode song)
        {
            if (song == null || !File.Exists(song.FilePath)) return;

            axWindowsMediaPlayer.URL = song.FilePath;
            axWindowsMediaPlayer.Ctlcontrols.play();

            lblNowPlaying.Text = song.FileName;
            SetAlbumPlaying();

            // THÊM DÒNG NÀY
            StartMarquee();
        }



        private void SetAlbumPlaying()
        {
            string path = Path.Combine(Application.StartupPath, "Images", "disc_playing.gif");
            if (File.Exists(path))
                picAlbum.Image = Image.FromFile(path);
        }

        private void SetAlbumIdle()
        {
            string path = Path.Combine(Application.StartupPath, "Images", "disc_idle.png");
            if (File.Exists(path))
                picAlbum.Image = Image.FromFile(path);
        }

        private void RefreshList()
        {
            lstSongs.Items.Clear();
            foreach (string name in playlist.GetAllNames())
                lstSongs.Items.Add(name);
        }

        private void AxWindowsMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if ((WMPLib.WMPPlayState)e.newState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                SongNode next = playlist.Next();
                if (next != null)
                {
                    BeginInvoke(new Action(() =>
                    {
                        PlaySong(next);
                    }));
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            marqueeTimer.Interval = 200;
            marqueeTimer.Tick += MarqueeTimer_Tick;
            timerProgress.Interval = 500; // cập nhật mỗi 0.5s
            timerProgress.Tick += TimerProgress_Tick;
            timerProgress.Start();
        }
        private void TimerProgress_Tick(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer.currentMedia != null && axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                double currentSec = axWindowsMediaPlayer.Ctlcontrols.currentPosition;
                double totalSec = axWindowsMediaPlayer.currentMedia.duration;

                TimeSpan current = TimeSpan.FromSeconds(currentSec);
                TimeSpan total = TimeSpan.FromSeconds(totalSec);

                lblTime.Text = $"{current.Minutes:D2}:{current.Seconds:D2} / {total.Minutes:D2}:{total.Seconds:D2}";
            }
            else if (axWindowsMediaPlayer.currentMedia != null)
            {
                double totalSec = axWindowsMediaPlayer.currentMedia.duration;
                TimeSpan total = TimeSpan.FromSeconds(totalSec);
                lblTime.Text = $"00:00 / {total.Minutes:D2}:{total.Seconds:D2}";
            }
            else
            {
                lblTime.Text = "00:00 / 00:00";
            }
        }

        private void MarqueeTimer_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(marqueeText)) return;

            marqueeText = marqueeText.Substring(1) + marqueeText[0];
            lblNowPlaying.Text = marqueeText;
        }
        private void StartMarquee()
        {
            if (string.IsNullOrWhiteSpace(lblNowPlaying.Text)) return;

            marqueeText = "🎵 " + lblNowPlaying.Text + "     ";
            marqueeTimer.Start();
        }
        private void UpdateSongCount()
        {
            lblSongCount.Text = $"🎵 Songs: {playlist.Count}";
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {

        }
    }
}
