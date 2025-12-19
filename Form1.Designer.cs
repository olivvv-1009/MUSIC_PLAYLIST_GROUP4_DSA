using System.Drawing;
using System.Windows.Forms;

namespace CTDL
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelPlaylist = new System.Windows.Forms.Panel();
            this.lblSongCount = new System.Windows.Forms.Label();
            this.lblPlaylist = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lstSongs = new System.Windows.Forms.ListBox();
            this.btnAddSong = new System.Windows.Forms.Button();
            this.btnRemoveSong = new System.Windows.Forms.Button();
            this.btnSortByName = new System.Windows.Forms.Button();
            this.btnShuffle = new System.Windows.Forms.Button();
            this.btnReverse = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.picAlbum = new System.Windows.Forms.PictureBox();
            this.lblNowPlaying = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.panelPlaylist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAlbum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPlaylist
            // 
            this.panelPlaylist.BackColor = System.Drawing.Color.White;
            this.panelPlaylist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPlaylist.Controls.Add(this.lblSongCount);
            this.panelPlaylist.Controls.Add(this.lblPlaylist);
            this.panelPlaylist.Controls.Add(this.txtSearch);
            this.panelPlaylist.Controls.Add(this.lstSongs);
            this.panelPlaylist.Location = new System.Drawing.Point(20, 20);
            this.panelPlaylist.Name = "panelPlaylist";
            this.panelPlaylist.Size = new System.Drawing.Size(358, 400);
            this.panelPlaylist.TabIndex = 0;
            // 
            // lblSongCount
            // 
            this.lblSongCount.BackColor = System.Drawing.Color.Transparent;
            this.lblSongCount.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSongCount.ForeColor = System.Drawing.Color.DimGray;
            this.lblSongCount.Location = new System.Drawing.Point(20, 363);
            this.lblSongCount.Name = "lblSongCount";
            this.lblSongCount.Size = new System.Drawing.Size(320, 25);
            this.lblSongCount.TabIndex = 0;
            this.lblSongCount.Text = "🎵 Songs: 0";
            this.lblSongCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPlaylist
            // 
            this.lblPlaylist.Location = new System.Drawing.Point(3, 2);
            this.lblPlaylist.Name = "lblPlaylist";
            this.lblPlaylist.Size = new System.Drawing.Size(352, 30);
            this.lblPlaylist.TabIndex = 0;
            this.lblPlaylist.Text = "My Playlist";
            this.lblPlaylist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(20, 40);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(320, 30);
            this.txtSearch.TabIndex = 1;
            // 
            // lstSongs
            // 
            this.lstSongs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstSongs.ItemHeight = 23;
            this.lstSongs.Location = new System.Drawing.Point(20, 80);
            this.lstSongs.Name = "lstSongs";
            this.lstSongs.Size = new System.Drawing.Size(320, 280);
            this.lstSongs.TabIndex = 2;
            // 
            // btnAddSong
            // 
            this.btnAddSong.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnAddSong.Location = new System.Drawing.Point(20, 430);
            this.btnAddSong.Name = "btnAddSong";
            this.btnAddSong.Size = new System.Drawing.Size(50, 45);
            this.btnAddSong.TabIndex = 1;
            this.btnAddSong.Text = "➕";
            // 
            // btnRemoveSong
            // 
            this.btnRemoveSong.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnRemoveSong.Location = new System.Drawing.Point(88, 430);
            this.btnRemoveSong.Name = "btnRemoveSong";
            this.btnRemoveSong.Size = new System.Drawing.Size(50, 45);
            this.btnRemoveSong.TabIndex = 2;
            this.btnRemoveSong.Text = "❌";
            // 
            // btnSortByName
            // 
            this.btnSortByName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnSortByName.Location = new System.Drawing.Point(154, 430);
            this.btnSortByName.Name = "btnSortByName";
            this.btnSortByName.Size = new System.Drawing.Size(50, 45);
            this.btnSortByName.TabIndex = 3;
            this.btnSortByName.Text = "A→Z";
            // 
            // btnShuffle
            // 
            this.btnShuffle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnShuffle.Location = new System.Drawing.Point(228, 430);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(50, 45);
            this.btnShuffle.TabIndex = 4;
            this.btnShuffle.Text = "🔀";
            // 
            // btnReverse
            // 
            this.btnReverse.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnReverse.Location = new System.Drawing.Point(298, 430);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(50, 45);
            this.btnReverse.TabIndex = 5;
            this.btnReverse.Text = "🔁";
            // 
            // btnPlay
            // 
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnPlay.Location = new System.Drawing.Point(412, 441);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(30, 35);
            this.btnPlay.TabIndex = 6;
            this.btnPlay.Text = "▶";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnPrevious.Location = new System.Drawing.Point(467, 442);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(31, 33);
            this.btnPrevious.TabIndex = 7;
            this.btnPrevious.Text = "⏮";
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnNext.Location = new System.Drawing.Point(494, 442);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 33);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "⏭";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click_1);
            // 
            // picAlbum
            // 
            this.picAlbum.Location = new System.Drawing.Point(412, 20);
            this.picAlbum.Name = "picAlbum";
            this.picAlbum.Size = new System.Drawing.Size(360, 360);
            this.picAlbum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAlbum.TabIndex = 9;
            this.picAlbum.TabStop = false;
            // 
            // lblNowPlaying
            // 
            this.lblNowPlaying.AutoEllipsis = true;
            this.lblNowPlaying.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNowPlaying.Location = new System.Drawing.Point(412, 394);
            this.lblNowPlaying.Name = "lblNowPlaying";
            this.lblNowPlaying.Size = new System.Drawing.Size(360, 26);
            this.lblNowPlaying.TabIndex = 10;
            this.lblNowPlaying.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // axWindowsMediaPlayer
            // 
            this.axWindowsMediaPlayer.Enabled = true;
            this.axWindowsMediaPlayer.Location = new System.Drawing.Point(412, 430);
            this.axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            this.axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer.OcxState")));
            this.axWindowsMediaPlayer.Size = new System.Drawing.Size(360, 45);
            this.axWindowsMediaPlayer.TabIndex = 11;
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnStop.Location = new System.Drawing.Point(435, 443);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(35, 32);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "⏹";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTime.ForeColor = System.Drawing.Color.Black;
            this.lblTime.Location = new System.Drawing.Point(663, 449);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(100, 20);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "00:00 / 00:00";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnAddSong);
            this.Controls.Add(this.btnRemoveSong);
            this.Controls.Add(this.btnSortByName);
            this.Controls.Add(this.btnShuffle);
            this.Controls.Add(this.btnReverse);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.picAlbum);
            this.Controls.Add(this.lblNowPlaying);
            this.Controls.Add(this.panelPlaylist);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.axWindowsMediaPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Music Player";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelPlaylist.ResumeLayout(false);
            this.panelPlaylist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAlbum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPlaylist;
        private System.Windows.Forms.Label lblPlaylist;
        private System.Windows.Forms.ListBox lstSongs;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAddSong;
        private System.Windows.Forms.Button btnRemoveSong;
        private System.Windows.Forms.Button btnSortByName;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.PictureBox picAlbum;
        private System.Windows.Forms.Label lblNowPlaying;
        private System.Windows.Forms.Label lblSongCount;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;


    }
}
