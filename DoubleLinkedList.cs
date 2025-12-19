using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace CTDL
{

    // LỚP DANH SÁCH LIÊN KẾT ĐÔI
    internal class DoublyLinkedList
    {
        public SongNode Head { get; set; }
        public SongNode Tail { get; set; }
        public SongNode Current { get; set; }
        public int Count { get; private set; }

        // Find: Tìm Node chứa bài hát 
        private SongNode Find(string songName)
        {
            SongNode current = Head;
            while (current != null)
            {
                if (current.FileName.Equals(songName, StringComparison.OrdinalIgnoreCase))
                {
                    return current;
                }
                current = current.Next; 
            }
            return null;
        }

        private void SwapData(SongNode a, SongNode b)
        {
            string tmpName = a.FileName; string tmpPath = a.FilePath;
            a.FileName = b.FileName; a.FilePath = b.FilePath;
            b.FileName = tmpName; b.FilePath = tmpPath;
        }

        //CÁC HÀM CƠ BẢN (ADD, REMOVE)

        public void AddSong(string filePath, string fileName)
        {
            SongNode node = new SongNode(filePath, fileName);
            if (Head == null) { Head = Tail = node; }
            else
            {
                Tail.Next = node;
                node.Previous = Tail;
                Tail = node;
            }
            Count++;
        }


        // Remove: Xóa theo Tên 
        public void Remove(string songName)
        {
            SongNode current = Find(songName);
            if (current == null) return;

            if (current.Next != null)
            {
                current.Next.Previous = current.Previous;
            }
            else
            {
                Tail = current.Previous;
            }

            if (current.Previous != null)
            {
                current.Previous.Next = current.Next;
            }
            else
            {
                Head = current.Next;
            }

            // Xử lý node đang phát
            if (Current == current) Current = current.Next ?? current.Previous;

            current.Next = null;
            current.Previous = null;

            Count--;
        }

        //CHỨC NĂNG TÌM KIẾM (SEQUENTIAL)

        // Sequential Search
        public int SequentialSearch(string keyword)
        {
            SongNode cur = Head;
            int index = 0;
            while (cur != null)
            {
                if (cur.FileName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    return index;
                cur = cur.Next;
                index++;
            }
            return -1;
        }

        // CHỨC NĂNG SẮP XẾP (INSERTION)

        // Insertion Sort
        public void InsertionSort()
        {
            if (Count < 2) return;

            // Lấy thông tin so sánh theo văn hóa tiếng Việt
            CompareInfo comparer = CultureInfo.GetCultureInfo("vi-VN").CompareInfo;

            SongNode cur = Head.Next;
            while (cur != null)
            {
                SongNode key = cur;
                SongNode prev = cur.Previous;

                // Tráo đổi dữ liệu ngược về đầu danh sách theo thứ tự tiếng Việt
                while (prev != null && comparer.Compare(prev.FileName, key.FileName, CompareOptions.None) > 0)
                {
                    SwapData(prev, key);
                    key = prev;
                    prev = prev.Previous;
                }
                cur = cur.Next;
            }
        }

        //SHUFFLE
        public void Shuffle()
        {
            if (Count <= 1) return;

            List<SongNode> nodes = new List<SongNode>();
            SongNode cur = Head;
            while (cur != null)
            {
                nodes.Add(cur);
                cur = cur.Next;
            }

            Random rnd = new Random();
            nodes = nodes.OrderBy(x => rnd.Next()).ToList();

            Rebuild(nodes);
        }
        //REVERSE
        public void Reverse()
        {
            SongNode cur = Head;
            SongNode temp = null;

            while (cur != null)
            {
                temp = cur.Previous;
                cur.Previous = cur.Next;
                cur.Next = temp;
                cur = cur.Previous;
            }

            temp = Head;
            Head = Tail;
            Tail = temp;
        }
        //REBUILD
        private void Rebuild(List<SongNode> nodes)
        {
            Head = Tail = null;
            Count = 0;

            foreach (var n in nodes)
                AddSong(n.FilePath, n.FileName);
        }
        // CÁC HÀM TIỆN ÍCH KHÁC

        public SongNode GetAt(int index)
        {
            if (index < 0 || index >= Count) return null;
            SongNode cur = Head;
            int i = 0;
            while (cur != null) { if (i == index) return cur; cur = cur.Next; i++; }
            return null;
        }


        public void MoveNext()
        {
            if (Current != null)
            {
                Current = Current.Next;
            }
        }

        public void MovePrevious()
        {
            if (Current != null)
            {
                Current = Current.Previous;
            }
        }
        public SongNode HeadNode => Head;
    }
}