using System;
using System.Windows.Forms;

namespace CTDL
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args) 
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Kiểm tra xem có lệnh "test" được truyền vào không
            if (args.Length > 0 && args[0] == "test")
            {
                // Chạy thuật toán
                AlgorithmComparison algo = new AlgorithmComparison();
                algo.RunComparison();
                return;
            }

            // Vào App nghe nhạc
            Application.Run(new Form1());
        }
    }
}