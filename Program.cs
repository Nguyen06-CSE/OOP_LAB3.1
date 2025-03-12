using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LAB3._1
{
    internal class Program
    {
        public enum ThucDon
        {
            NhapTuFile = 1,
            Xuat,
            TimDSCacThanhPho,
            DemSoThueBaoTheoTP,
            TimTPCoNhieuThueBaoNhat,
            Thoat
        }
        static void Main(string[] args)
        {
            //DanhBa db = new DanhBa();
            //db.NhapTuFile();
            //db.Xuat();

            ////tao 1 danh sach string de luu ket qua 
            //List<string> kq = db.TimDSCacThanhPho();
            ////duyet qua mang de in
            //foreach (var item in kq)
            //{
            //    Console.WriteLine($"Thanh pho {item}");
            //}

            //tao danh sach thanh pho
            //List<string> kq = db.TimDSCacThanhPho();
            ////duyet qua mang de xat ra man hinh
            //foreach (var item in kq)
            //{
            //    Console.WriteLine(item + " so thue bao " + db.DemSoThueBaoTheoTP(item));
            //}

            //tao danh sach cac thanh pho co nhieu thue bao nhat
            //List<string> kq1 = db.TimTPCoNhieuThueBaoNhat();
            ////duyet qua mnag va in
            //foreach(var i in kq1)
            //{
            //    Console.WriteLine(i + " so thue bao lon nhat " + db.DemSoThueBaoTheoTP(i));
            //}


            DanhBa db = new DanhBa();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Chon chuc nang:");
                Console.WriteLine($"Nhap ({(int)ThucDon.NhapTuFile}) de nhap tu file");
                Console.WriteLine($"Xuat ({(int)ThucDon.Xuat}) de xuat danh ba");
                Console.WriteLine($"TimDSCacThanhPho ({(int)ThucDon.TimDSCacThanhPho}) de tim danh sach cac thanh pho");
                Console.WriteLine($"DemSoThueBaoTheoTP ({(int)ThucDon.DemSoThueBaoTheoTP}) de dem so thue bao theo thanh pho");
                Console.WriteLine($"TimTPCoNhieuThueBaoNhat ({(int)ThucDon.TimTPCoNhieuThueBaoNhat}) de tim thanh pho co nhieu thue bao nhat");
                Console.WriteLine($"Thoat ({(int)ThucDon.Thoat}) de thoat");

                ThucDon chon = (ThucDon)int.Parse(Console.ReadLine());

                switch (chon)
                {
                    case ThucDon.NhapTuFile:
                        db.NhapTuFile();
                        break;
                    case ThucDon.Xuat:
                        db.Xuat();
                        break;
                    case ThucDon.TimDSCacThanhPho:
                        List<string> kq = db.TimDSCacThanhPho();
                        foreach (var item in kq)
                        {
                            Console.WriteLine($"Thanh pho {item}");
                        }
                        break;
                    case ThucDon.DemSoThueBaoTheoTP:
                        List<string> kq1 = db.TimDSCacThanhPho();
                        foreach (var item in kq1)
                        {
                            Console.WriteLine(item + " so thue bao " + db.DemSoThueBaoTheoTP(item));
                        }
                        break;
                    case ThucDon.TimTPCoNhieuThueBaoNhat:
                        List<string> kq2 = db.TimTPCoNhieuThueBaoNhat();
                        foreach (var item in kq2)
                        {
                            Console.WriteLine(item + " so thue bao lon nhat " + db.DemSoThueBaoTheoTP(item));
                        }
                        break;
                    default:
                        return;
                }

                Console.WriteLine("Bam 1 phim de tiep tuc");
                Console.ReadKey();
            }
        }
    }
}
