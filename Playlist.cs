using System.Collections.Generic;

namespace CTDL
{
    internal class Playlist
    {
        // Đối tượng danh sách liên kết kép quản lý các Node
        public DoublyLinkedList list = new DoublyLinkedList();


        public SongNode Current => list.Current;

        //CRUD (Thêm, Xóa, Sửa)
        public void AddSong(string path, string name)
        {
            list.AddSong(path, name);
        }

        // Xóa bài hát theo index 
        public void RemoveAt(int index)
        {

            // Lấy tên bài hát ở index cần xóa
            SongNode nodeToRemove = list.GetAt(index);
            if (nodeToRemove != null)
            {
                // Gọi hàm Remove đã được chuẩn hóa theo Tên
                list.Remove(nodeToRemove.FileName);
            }
        }

        public SongNode PlayAt(int index)
        {
            list.Current = list.GetAt(index);
            return list.Current;
        }


        //PLAY (NEXT)
        public SongNode Next()
        {
            if (list.Current == null)
            {
                list.Current = list.Head;
            }
            else
            {
                // 2. Chuyển sang bài kế
                list.MoveNext();
                if (list.Current == null)
                {
                    list.Current = list.Head;
                }
            }
            return list.Current;
        }

        // PLAY (PREVIOUS)
        public SongNode Previous()
        {
            if (list.Current == null)
            {
                // 1. Nếu chưa phát, bắt đầu từ Tail
                list.Current = list.Tail;
            }
            else
            {
                // 2. Lùi về bài trước
                list.MovePrevious();
                if (list.Current == null)
                {
                    list.Current = list.Tail;
                }
            }
            return list.Current;
        }

        // SEARCH (SEQUENTIAL SEARCH) 
        public int SequentialSearch(string keyword)
        {
            return list.SequentialSearch(keyword);
        }

        //  SORT (INSERTION SORT) 
        public void InsertionSort() => list.InsertionSort();

        //SHUFFLE AND REVERSE
        public void Shuffle() => list.Shuffle();
        public void Reverse() => list.Reverse();

        //DATA (Dữ liệu & Cần cho ListBox)
        public int Count => list.Count;
        public List<string> ToList()
        {
            List<string> results = new List<string>();
            SongNode current = list.Head;

            while (current != null)
            {
                results.Add(current.FileName);
                current = current.Next;
            }
            return results;
        }

        public List<string> GetAllNames()
        {
            return ToList();
        }
    }
}