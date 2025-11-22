# 📝 Smart Task Management System

A modern, user-friendly Task Management System built using **ASP.NET MVC**, **C#**, **Entity Framework**, and **Bootstrap 5**.  
The system helps Managers assign tasks, review progress, and Employees manage their work efficiently.

---

## 🚀 Features

### 👨‍💼 Manager Module
- Assign tasks to employees.
- Set status: *ToDo*, *Doing*, *Hold*, *Done*.
- Review employee task submissions.
- Add review comments.
- View department-wide tasks.
- Built-in search/filter for employee tasks.

### 👨‍🔧 Employee Module
- View assigned tasks.
- Update progress/status.
- Submit work for review.
- Edit task details.

### 🔐 Authentication
- Login system for Manager and Employee.
- Secure password handling.
- Email-based login.

---

## 🎨 UI & Layout

- Fully responsive using **Bootstrap 5.3**
- Clean, minimal dashboard UI
- Uniform View design using a shared `_Layout.cshtml`
- Full-width card layout with no extra page gaps
- Smooth animations and modern color palette

---

## 🛠️ Tech Stack

| Technology | Purpose |
|-----------|---------|
| **ASP.NET MVC 5** | Web application framework |
| **C#** | Backend logic |
| **Entity Framework** | ORM & database access |
| **Bootstrap 5** | UI Framework |
| **SQL Server** | Database |
| **LINQ** | Data queries |
| **Git / GitHub** | Version control |

---

## 📂 Project Structure

SmartTaskManagement/
│
├── App_Data/
│
├── App_Start/
│   ├── BundleConfig.cs
│   ├── FilterConfig.cs
│   └── RouteConfig.cs
│
├── Content/
│   ├── CSS files / Bootstrap theme / Custom styles
│
├── Controllers/
│   ├── AccountController.cs
│   ├── EmployeeController.cs
│   └── ManagerController.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Filters/
│   └── (Custom authorization filters if any)
│
├── Helpers/
│   └── SessionKeys.cs
│
├── Migrations/
│   ├── 202511221107385_Init.cs
│   └── Configuration.cs
│
├── Models/
│   ├── TaskItem.cs
│   ├── User.cs
│   └── ViewModels/
│       ├── AssignTaskViewModel.cs
│       ├── LoginViewModel.cs
│       ├── SignUpViewModel.cs
│       └── ViewTasksViewModel.cs
│
├── Scripts/
│   └── jQuery / Bootstrap JS / Validation files
│
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml
│   │   └── SignUp.cshtml
│   │
│   ├── Employee/
│   │   ├── Index.cshtml
│   │   ├── EditTask.cshtml
│   │   └── ViewTasks.cshtml
│   │
│   ├── Manager/
│   │   ├── AssignTask.cshtml
│   │   ├── ReviewTask.cshtml
│   │   └── ViewDepartmentTasks.cshtml
│   │
│   └── Shared/
│       ├── _Layout.cshtml
│       ├── _ViewStart.cshtml
│       └── Error.cshtml
│
├── Global.asax
├── README.md
├── favicon.ico
├── packages.config
└── Web.config

---

## 📧 Contact

**Developer:** Sonaly Ganapathy  
📩 **Email:** iamsonaly@gmail.com 
🐙 **GitHub:** https://github.com/SonalyGanapathy
🔗 **LinkedIn:** https://www.linkedin.com/in/sonaly-ganapathy-4007a7230/

If you have any questions, suggestions, or feedback, feel free to reach out!


