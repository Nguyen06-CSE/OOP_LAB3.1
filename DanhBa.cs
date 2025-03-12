using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3._1
{

    public enum TimKieu
    {
        TimMax,
        TimMin
    }


    internal class DanhBa
    {
        //khởi tạo 1 list ThueBao để đọc các thông tin trong lớp ThueBao 
        List<ThueBao> collection = new List<ThueBao>();


        //hàm xuất 
        public void Xuat()
        {
            //duyệt qua tất cả các phần tử ThueBao của list collection mớii tạo ở trên 
            foreach (var item in collection)
            {
                //khi truy cập vào mỗi ThueBao thì sẽ gọi hàm Xuat cua lớp ThueBao 
                item.Xuat();
            }
        }

        //Hàm để thêm ThueBao vào danh sách 
        public void Them(ThueBao n)
        {
            collection.Add(n);
        }

        //hàm để đọc file tên thư mục 
        public void NhapTuFile()
        {
            //tạo thư mục data.csv ở trong DEBUG và tạo 1 string có đuòng dẫn của file để máy có thể dọc dữ liệu 
            string tenFile = "C:\\Users\\nguyen.cao\\Desktop\\codec++\\oop\\baiTapTrenLMS\\lab1.1\\LAB3.1\\obj\\Debug\\data.csv";
            StreamReader sr = new StreamReader(tenFile);
            //tạo 1 sring để lưu thông tin vừa đọc được 
            string s = "";
            while((s = sr.ReadLine()) != null)
            {
                //tạo 1 thuê bao mới và gọi ThueBao(s) (hàm khởi tạo được xây dựng riêng để xử lí file)
                ThueBao n = new ThueBao(s);
                //thêm vào collection 
                collection.Add(n);
            }
        }
        public int TimGiaTri(int giaTri, int current, TimKieu kieu)
        {
            return (kieu == TimKieu.TimMax) ? Math.Max(giaTri, current) : Math.Min(giaTri, current);
        }



        public List<string> TimDSCacThanhPho()
        {
            //tao 1 danh sach string de luu ket qua
            List<string> kq = new List<string>();
            //duyet qua danh sach
            foreach (var item in collection)
            {
                //neu danh sach kq chua chua dung thueBao.ThanhPho thi se them vao
                //muc dich nham them tat ca cac thanh pho trong danh sach ma khong trung nhau
                if(!kq.Contains(item.ThanhPho))
                {
                    kq.Add(item.ThanhPho);
                }
            }
            return kq;
        }

        public int DemSoThueBaoTheoTP(string tp)
        {

            //tao bien dem de luu ket qua
            int dem = 0;
            //duyet qua danh sach
            foreach (var item in collection)
            {
                //neu tp(cho truoc) bang ThanhPho trong mang thi cong bien dem
                if(item.ThanhPho == tp)
                {
                    ++dem;
                }
            }
            return dem;
        }

        public List<string> TimTPCoNhieuThueBaoNhat()
        {
            //tao danh sach luu ket qua
            List<string> kq = new List<string>();
            //tao danh sach luu cac thanh pho
            List<string> dstp = TimDSCacThanhPho();
            //tao gia tri nho nhat de so sanh
            int max = int.MinValue;
            //duyet qua mang
            foreach(var item in dstp)
            {
                ////tim max
                //if (DemSoThueBaoTheoTP(item) < max)
                //max = DemSoThueBaoTheoTP(item);
                max = TimGiaTri(DemSoThueBaoTheoTP(item), max, TimKieu.TimMax);
            }
            //Console.WriteLine(" Max = " + max);
            //duyer qua mang
            foreach(var item in dstp)
            {
                //neu so thue bao cua 1 thanh pho bang max thi se luu vao danh sach ket qua
                if(DemSoThueBaoTheoTP(item) == max)
                {
                    kq.Add(item);
                }
            }
            return kq;
        }


        //public List<string> TimThanhPhoCoSoThueBaoLaX(string x)
        //{
        //    List<string> result = new List<string>();

        //    foreach (var thueBao in collection)
        //    {
        //        if (thueBao. == x)
        //        {
        //            result.Add(thueBao.ThanhPho);
        //        }
        //    }

        //    return result.Distinct().ToList(); // Loại bỏ các thành phố trùng nhau
        //}


        public ThueBao TimThueBaoItSoNhat()
        {
            ThueBao minThueBao = collection[0];
            foreach (var tb in collection)
            {
                if (tb.DemSoLuongSoDT() < minThueBao.DemSoLuongSoDT())
                {
                    minThueBao = tb;
                }
            }
            return minThueBao;
        }

        // Generic swap function that works for any list type
        public void Swap<T>(List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public void SapXepThueBao()
        {
            for (int i = 0; i < collection.Count - 1; i++)
            {
                for (int j = i + 1; j < collection.Count; j++)
                {
                    if (string.Compare(collection[i].HoTen, collection[j].HoTen) > 0 ||
                        (collection[i].HoTen == collection[j].HoTen && collection[i].DemSoLuongSoDT() < collection[j].DemSoLuongSoDT()))
                    {
                        Swap(collection, i, j);
                    }
                }
            }
        }

        public List<string> DanhSachThanhPhoSapXep()
        {
            List<string> dstp = TimDSCacThanhPho();
            for (int i = 0; i < dstp.Count - 1; i++)
            {
                for (int j = i + 1; j < dstp.Count; j++)
                {
                    if (DemSoThueBaoTheoTP(dstp[i]) < DemSoThueBaoTheoTP(dstp[j]) ||
                        (DemSoThueBaoTheoTP(dstp[i]) == DemSoThueBaoTheoTP(dstp[j]) && string.Compare(dstp[i], dstp[j]) > 0))
                    {
                        Swap(dstp, i, j);
                    }
                }
            }
            return dstp;
        }

        public void SapXepDanhBa()
        {
            for (int i = 0; i < collection.Count - 1; i++)
            {
                for (int j = i + 1; j < collection.Count; j++)
                {
                    if (string.Compare(collection[i].HoTen, collection[j].HoTen) > 0 ||
                        (collection[i].HoTen == collection[j].HoTen && collection[i].NgaySinh > collection[j].NgaySinh) ||
                        (collection[i].HoTen == collection[j].HoTen && collection[i].NgaySinh == collection[j].NgaySinh && string.Compare(collection[i].ThanhPho, collection[j].ThanhPho) > 0))
                    {
                        Swap(collection, i, j);
                    }
                }
            }
        }


        public List<int> TimThangKhongCoDangKy()
        {
            bool[] thangCoDangKy = new bool[13];
            foreach (var tb in collection)
            {
                thangCoDangKy[tb.NgayDangKy.Month] = true;
            }
            List<int> thangKhongCoDangKy = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                if (!thangCoDangKy[i])
                {
                    thangKhongCoDangKy.Add(i);
                }
            }
            return thangKhongCoDangKy;
        }

        public List<ThueBao> TimTheoGioiTinh(GioiTinh gioiTinh)
        {
            return collection.Where(tb => tb.GioiTinh == gioiTinh).ToList();
        }
    
        public void XoaTheoTinh(string tinh)
        {
            for(int i = 0; i < collection.Count; i++)
            {
                if (collection[i].DiaChi.CompareTo(tinh) == 0)
                {
                    collection.RemoveAt(i);
                }
            }
        }

        public void TangSoDienThoaiThang1()
        {
            foreach (var tb in collection)
            {
                if (tb.NgaySinh.Month == 1)
                {
                    tb.SoDT.Add(tb.SoCMND);
                }
            }
        }

        // Function 1: Count the number of registrations per date
        public Dictionary<DateTime, int> DemSoLuongDangKyTheoNgay()
        {
            Dictionary<DateTime, int> ngayCounts = new Dictionary<DateTime, int>();

            foreach (var tb in collection)
            {
                if (ngayCounts.ContainsKey(tb.NgayDangKy))
                    ngayCounts[tb.NgayDangKy]++;
                else
                    ngayCounts[tb.NgayDangKy] = 1;
            }

            return ngayCounts;
        }

        // Function 2: Find the days with the most and least registrations
        public List<DateTime> TimNgayDangKyNhieuVaItNhat()
        {
            Dictionary<DateTime, int> ngayCounts = DemSoLuongDangKyTheoNgay();

            // Step 1: Find max and min values
            int maxCount = int.MinValue;
            int minCount = int.MaxValue;

            foreach (var count in ngayCounts.Values)
            {
                maxCount = TimGiaTri(count, maxCount, TimKieu.TimMax);
                minCount = TimGiaTri(count, minCount, TimKieu.TimMin);
            }

            // Step 2: Collect all dates with max and min counts
            List<DateTime> result = new List<DateTime>();

            foreach (var entry in ngayCounts)
            {
                if (entry.Value == maxCount || entry.Value == minCount)
                    result.Add(entry.Key);
            }

            return result;
        }

    }
}
