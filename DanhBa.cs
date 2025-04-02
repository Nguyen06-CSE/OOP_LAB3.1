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



        //public Dictionary<DateTime, int> DemSoLuongDangKyTheoNgay()
        //{
        //    //tao mot Dictionary de duyet qua mang khong trung lap cac phan tu da duyet roi
        //    Dictionary<DateTime, int> ngayCounts = new Dictionary<DateTime, int>();

        //    foreach (var tb in collection)
        //    {
        //        //o day ngayCount chi chua cac ngay khac nhau, va neu tim thay ngay nao trung thi se tang bien dem len
        //        if (ngayCounts.ContainsKey(tb.NgayDangKy))
        //            ngayCounts[tb.NgayDangKy]++;
        //        else
        //            //neu khong thi chi co 1 ngay 
        //            ngayCounts[tb.NgayDangKy] = 1;
        //    }

        //    return ngayCounts;
        //}

        //public List<DateTime> TimNgayDangKyNhieuVaItNhat()
        //{

        //    Dictionary<DateTime, int> ngayCounts = DemSoLuongDangKyTheoNgay();

        //    // tim max, min
        //    int maxCount = int.MinValue;
        //    int minCount = int.MaxValue;

        //    foreach (var count in ngayCounts.Values)
        //    {
        //        //duyet qua mang de tim nhung gia tri bang max va min
        //        maxCount = TimGiaTri(count, maxCount, TimKieu.TimMax);
        //        minCount = TimGiaTri(count, minCount, TimKieu.TimMin);
        //    }

        //    // them cac ngay bang max va min vao trong ket qua
        //    List<DateTime> result = new List<DateTime>();

        //    foreach (var entry in ngayCounts)
        //    {
        //        if (entry.Value == maxCount || entry.Value == minCount)
        //            result.Add(entry.Key);
        //    }

        //    return result;
        //}

        public List<DateTime> LoaiBoNgayTrung()
        {
            List<DateTime> ngayKhongTrung = new List<DateTime>();

            foreach (var tb in collection)
            {
                //neu trong mang khong co chua ngay dang ky thi se them vao de loai bo cac phan tu trung lap
                if (!ngayKhongTrung.Contains(tb.NgayDangKy))
                {
                    ngayKhongTrung.Add(tb.NgayDangKy);
                }
            }

            return ngayKhongTrung;
        }

        // Hàm đếm số lượng đăng ký theo ngày
        public List<(DateTime, int)> DemSoLuongDangKyTheoNgay()
        {
            List<DateTime> ngayKhongTrung = LoaiBoNgayTrung();
            List<(DateTime, int)> ketQua = new List<(DateTime, int)>();

            foreach (var ngay in ngayKhongTrung)
            {
                int count = 0;
                foreach (var tb in collection)
                {
                    if (tb.NgayDangKy == ngay)
                    {
                        count++;
                    }
                }
                ketQua.Add((ngay, count));
            }

            return ketQua;
        }

        // Hàm tìm ngày có số lượng đăng ký nhiều nhất và ít nhất
        public void TimNgayDangKyNhieuNhat()
        {
            var ngayCounts = DemSoLuongDangKyTheoNgay();

            int maxCount = int.MinValue;

            foreach (var entry in ngayCounts)
            {
                maxCount = TimGiaTri(entry.Item2, maxCount, TimKieu.TimMax);
            }

            Console.WriteLine("Các ngày có số lượng đăng ký nhiều nhất:");
            foreach (var entry in ngayCounts)
            {
                if (entry.Item2 == maxCount)
                {
                    Console.WriteLine(entry.Item1);
                }
            }
        }
        public void TimNgayDangKyItNhat()
        {
            var ngayCounts = DemSoLuongDangKyTheoNgay();

            int minCount = int.MaxValue;

            foreach (var entry in ngayCounts)
            {
                minCount = TimGiaTri(entry.Item2, minCount, TimKieu.TimMin);
            }

            Console.WriteLine("Các ngày có số lượng đăng ký ít nhất:");
            foreach (var entry in ngayCounts)
            {
                if (entry.Item2 == minCount)
                {
                    Console.WriteLine(entry.Item1);
                }
            }
        }

        public List<string> LoaiBoTPTrung()
        {
            List<string> TPKoTrung = new List<string>();

            foreach (var tb in collection)
            {
                if (!TPKoTrung.Contains(tb.DiaChi))   
                {
                    TPKoTrung.Add(tb.DiaChi);
                }
            }

            return TPKoTrung;
        }

        public void XuatThongTinThueBao(string TP)
        {
            foreach (var tb in collection)
            {
                if (tb.DiaChi == TP)  
                {
                    tb.Xuat();
                }
            }
        }

        public int DemSoLuongTP(string tinh)
        {
            int dem = 0;
            foreach (var item in collection)
            {
                if (item.DiaChi == tinh)  
                {
                    ++dem;
                }
            }
            return dem;
        }

        public void ThongKeTheoTungThanhPho()
        {
            List<string> uniqueCities = LoaiBoTPTrung();
            foreach (var city in uniqueCities)
            {
                Console.WriteLine($"Thành phố: {city} (Tổng số thuê bao: {DemSoLuongTP(city)})");
                XuatThongTinThueBao(city);
            }
        }

        public List<(string, int)> DemSoLuongThueBao(string loaiThueBao)
        {
            List<string> TPKhongTrung = LoaiBoTPTrung();
            List<(string, int)> ketQua = new List<(string, int)>();

            foreach (var TP in TPKhongTrung)
            {
                int count = 0;
                foreach (var tb in collection)
                {
                    if (tb.ThanhPho == TP && tb.LoaiThueBao == loaiThueBao  )
                    {
                        count++;
                    }
                }
                ketQua.Add((TP, count));
            }

            return ketQua;
        }
        public List<string> TimThanhPhoTheoDieuKien(string loaiThueBao, TimKieu kieu)
        {
            var thanhPhoCounts = DemSoLuongThueBao(loaiThueBao);

            int extremeCount = (kieu == TimKieu.TimMax) ? int.MinValue : int.MaxValue;
            List<string> result = new List<string>();

            foreach (var entry in thanhPhoCounts)
            {
                int newExtreme = TimGiaTri(extremeCount, entry.Item2, kieu);

                if (newExtreme != extremeCount) // If a new extreme value is found
                {
                    extremeCount = newExtreme;
                    result.Clear();
                    result.Add(entry.Item1);
                }
                else if (entry.Item2 == extremeCount) // If it's equal to the extreme value, add it
                {
                    result.Add(entry.Item1);
                }
            }

            return result;
        }

        // Using the refactored function
        public List<string> TimThanhPhoNhieuNhat(string loaiThueBao)
        {
            return TimThanhPhoTheoDieuKien(loaiThueBao, TimKieu.TimMax);
        }

        public List<string> TimThanhPhoItNhat(string loaiThueBao)
        {
            return TimThanhPhoTheoDieuKien(loaiThueBao, TimKieu.TimMin);
        }


        public void TimThueBaoItSoDienThoaiCoDinh()
        {
            ThueBao thueBaoItSDT = null;
            int minSDT = int.MaxValue;

            foreach (var tb in collection)
            {
                if (tb.LoaiThueBao == "CoDinh" && tb.SoDT.Count < minSDT)
                {
                    minSDT = tb.SoDT.Count;
                    thueBaoItSDT = tb;
                }
            }

            if (thueBaoItSDT != null)
            {
                Console.WriteLine("Thuê bao sở hữu ít số điện thoại cố định nhất:");
                thueBaoItSDT.Xuat();
            }
            else
            {
                Console.WriteLine("Không có thuê bao cố định nào.");
            }
        }

        public void TimThueBaoDiDongTheoGioiTinh(GioiTinh gioiTinh)
        {
            List<ThueBao> danhSach = new List<ThueBao>();

            foreach (var tb in collection)
            {
                if (tb.LoaiThueBao == "DiDong" && tb.GioiTinh == gioiTinh)
                {
                    danhSach.Add(tb);
                }
            }

            if (danhSach.Count > 0)
            {
                Console.WriteLine($"danh sach thue bao theo gioi tinh {gioiTinh}:");
                foreach (var tb in danhSach)
                {
                    tb.Xuat();
                }
            }
            else
            {
                Console.WriteLine($"danh sach rong.");
            }
        }

        public void XoaThueBaoTheoNgayLapDat(DateTime ngayLapDat)
        {
            int countBefore = collection.Count;
            collection.RemoveAll(tb => tb.NgayLapDat.Date == ngayLapDat.Date);

            int countAfter = collection.Count;
            Console.WriteLine($"Đã xóa {countBefore - countAfter} thuê bao có ngày lắp đặt {ngayLapDat.ToShortDateString()}.");
        }

        public void TimKhachHangDiDongTheoNhaCungCap(string nhaCungCap)
        {
            var danhSach = collection.Where(tb => tb.LoaiThueBao == "Di động" && tb.NhaDichVu == nhaCungCap).ToList();

            if (danhSach.Any())
            {
                Console.WriteLine($"Danh sách thuê bao di động của nhà cung cấp {nhaCungCap}:");
                foreach (var tb in danhSach)
                {
                    tb.Xuat();
                }
            }
            else
            {
                Console.WriteLine($"Không tìm thấy thuê bao di động nào thuộc nhà cung cấp {nhaCungCap}.");
            }
        }

        public void HienThiSoLuongThueBaoCoDinhTheoNhaCungCap()
        {
            Dictionary<string, int> soLuongTheoNhaCungCap = new Dictionary<string, int>();

            // Duyệt qua danh sách thuê bao để đếm số lượng thuê bao cố định theo nhà cung cấp
            foreach (var tb in collection)
            {
                if (tb.LoaiThueBao == "Cố định")
                {
                    if (!soLuongTheoNhaCungCap.ContainsKey(tb.NhaDichVu))
                    {
                        soLuongTheoNhaCungCap[tb.NhaDichVu] = 1;
                    }
                    else
                    {
                        soLuongTheoNhaCungCap[tb.NhaDichVu]++;
                    }
                }
            }

            // Hiển thị kết quả
            Console.WriteLine("Số lượng thuê bao cố định theo từng nhà cung cấp dịch vụ:");
            foreach (var kvp in soLuongTheoNhaCungCap)
            {
                Console.WriteLine($"Nhà cung cấp: {kvp.Key} - Số lượng: {kvp.Value}");
            }
        }


    }
}
