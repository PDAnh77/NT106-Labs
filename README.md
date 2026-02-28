# Bài thực hành Môn Lập trình mạng căn bản (NT106.O21.ANTT)

Đây là các bài tập thực hành môn Lập trình mạng căn bản **(NT106.O21.ANTT)**, sử dụng WinForms (C# – .NET Framework).

## Nội dung chính
- `Lab1` - Làm quen với WinForms: Tạo giao diện, xử lý sự kiện và các control cơ bản.
- `Lab2` - File và Stream I/O: đọc/ghi file văn bản, quản lý sinh viên với file nhị phân.
- `Lab3` - Lập trình Sockets: xây dựng ứng dụng Client-Server đơn giản sử dụng TCP/UDP, giao diện Dashboard quản lý kết nối.
- `Lab4` - Truyền thông với Web Server: lấy mã HTML, gửi dữ liệu POST, tải nội dung web, xây dựng trình duyệt mini.
- `Lab5` - Gửi và nhận email: xây dựng ứng dụng đọc email (IMAP) và gửi email (SMTP) sử dụng thư viện MailKit.
- `Lab6` - Ứng dụng Whiteboard: xây dựng app vẽ bảng trắng hỗ trợ nhiều client kết nối và tương tác thời gian thực.

## Mô tả
### Lab1
Gồm 5 bài tập nhỏ, làm quen với các control và xử lý sự kiện cơ bản trong WinForms:

- **Lab1-Bai1:** Tính tổng hai số nguyên, kiểm tra dữ liệu nhập, hiển thị kết quả và làm mới dữ liệu.
- **Lab1-Bai2:** Tìm giá trị lớn nhất, nhỏ nhất trong ba số thực nhập vào, kiểm tra dữ liệu nhập.
- **Lab1-Bai3:** Đọc số nguyên (0-9) thành chữ, kiểm tra dữ liệu nhập.
- **Lab1-Bai4:** Chuyển đổi qua lại giữa các hệ số: nhị phân, thập phân, thập lục phân.
- **Lab1-Bai5:** Nhập danh sách điểm, hiển thị điểm từng môn, tính điểm trung bình, xếp loại học lực, tìm điểm cao/thấp nhất, thống kê số môn đậu/rớt.
### Lab2
Gồm 5 bài tập nhỏ về thao tác file, xử lý văn bản và quản lý sinh viên:

- **Lab2-Bai1:** Đọc nội dung file văn bản, hiển thị lên RichTextBox, ghi nội dung (in hoa) trở lại file.
- **Lab2-Bai2:** Đọc file văn bản, đếm số dòng, số từ, số ký tự, hiển thị tên và đường dẫn file.
- **Lab2-Bai3:** Đọc file chứa phép tính, in ra kết quả phép tính.
- **Lab2-Bai4:** Quản lý sinh viên: nhập, lưu, đọc thông tin sinh viên từ file nhị phân, hiển thị danh sách và tính điểm trung bình.
- **Lab2-Bai5:** Hiển thị danh sách các file trong thư mục.
### Lab3
Gồm 4 bài tập nhỏ về lập trình mạng với các mô hình Client-Server, Listener, Dashboard:

- **Bai1:** Ứng dụng UDP Client-Server: gửi/nhận tin nhắn qua giao thức UDP, giao diện Dashboard quản lý kết nối.
- **Bai2:** Ứng dụng TCP Listener: lắng nghe, nhận và hiển thị dữ liệu từ client qua TCP, hiển thị thông tin kết nối.
- **Bai3:** Ứng dụng TCP Client-Server: client gửi tin nhắn, server nhận và phản hồi, giao diện Dashboard quản lý kết nối.
- **Bai4:** Ứng dụng TCP Chat nhiều client: server quản lý nhiều client, truyền nhận tin nhắn hai chiều, hiển thị trạng thái kết nối và lịch sử chat.
### Lab4
Gồm 4 bài tập về truyền thông với Web Server và xử lý nội dung web:

- **Bai1:** Lấy và hiển thị mã HTML của một trang web bất kỳ.
- **Bai2:** Gửi dữ liệu lên web server bằng phương thức POST, nhận và hiển thị phản hồi.
- **Bai3:** Tải nội dung HTML từ một trang web về máy, lưu file và hiển thị nội dung.
- **Bai4:** Trình duyệt web mini: duyệt trang web, xem mã nguồn, tải toàn bộ ảnh trên trang về máy.
### Lab5
Gồm 2 bài tập về lập trình ứng dụng email:

- **Bai2:** Xây dựng ứng dụng đọc email sử dụng IMAP (MailKit), đăng nhập, lấy danh sách email, xem nội dung chi tiết, xử lý lỗi kết nối.
- **Bai3:** Xây dựng ứng dụng gửi email sử dụng SMTP (MailKit), nhập thông tin người gửi/nhận, chủ đề, nội dung, gửi kèm file đính kèm.
### Lab6
Xây dựng ứng dụng Whiteboard đa client:

- **Client:** Vẽ bảng trắng, đồng bộ nét vẽ, màu, độ dày, undo/redo, gửi ảnh, lưu file, nhận trạng thái client khác.
- **Server:** Quản lý nhiều client, truyền nhận dữ liệu vẽ, cảnh báo khi đủ số client, gửi email thông báo cho admin.
- **Dashboard:** Giao diện quản lý, khởi động nhanh client/server.