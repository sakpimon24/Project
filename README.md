## Project HelpDesk

โปรเจกต์นี้เป็นระบบ HelpDesk แบบง่าย  
พัฒนาโดยใช้ **Windows Forms (C#)** และฐานข้อมูล **SQLite**  
สามารถจัดการข้อมูลสินค้า และออกใบเสร็จเป็น PDF ได้

---

## Features (ความสามารถ)

✅ เพิ่มข้อมูลสินค้า (Auto Generate ID)  
✅ แก้ไขข้อมูลสินค้าโดยอ้างอิง ProductID  
✅ ลบข้อมูลสินค้าโดยอ้างอิง ProductID  
✅ แสดงข้อมูลทั้งหมดในตาราง DataGridView  
✅ ออกใบเสร็จ PDF และบันทึกอัตโนมัติลงโฟลเดอร์ Downloads  

---

## Technology Stack

- C# Windows Forms App  
- SQLite Database (`mydatabase.db`)  
- iTextSharp (PDF Generator)  
- Visual Studio 2022+

---
## Use Case Diagram
<img width="832" height="775" alt="image" src="https://github.com/user-attachments/assets/c4cf5f76-6053-4715-8208-8f701d20bbf0" />

---
## Sequence Diagram Add Product
<img width="995" height="695" alt="image" src="https://github.com/user-attachments/assets/b6e85fb4-2356-4a28-8615-bb4a6d4727f1" />

---
## Sequence Diagram Generate Receipt PDF
<img width="832" height="775" alt="image" src="https://github.com/user-attachments/assets/c4cf5f76-6053-4715-8208-8f701d20bbf0" />

---


