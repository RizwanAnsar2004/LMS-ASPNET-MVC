-- Step 1: Create the Base Users Table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(100) NOT NULL,
    UserEmail NVARCHAR(100) NOT NULL UNIQUE,
    UserPassword NVARCHAR(100) NOT NULL,
    UserPhone NVARCHAR(15),
    UserAddress NVARCHAR(255),
    DateOfBirth DATE,
    RegistrationDate DATE DEFAULT GETDATE()
);

-- Step 2: Create the Majors Table
CREATE TABLE Majors (
    MajorID INT PRIMARY KEY IDENTITY,
    MajorName NVARCHAR(100) NOT NULL UNIQUE
);

-- Step 3: Create the Departments Table
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY,
    DepartmentName NVARCHAR(100) NOT NULL UNIQUE
);

-- Step 4: Create Separate Tables for Students, Faculty, and Admin
CREATE TABLE Students (
    StudentID INT PRIMARY KEY,
    UserID INT,
    EnrollmentDate DATE,
    MajorID INT,
    YearOfStudy INT,
    GPA DECIMAL(3, 2),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (MajorID) REFERENCES Majors(MajorID)
);

CREATE TABLE Faculty (
    FacultyID INT PRIMARY KEY,
    UserID INT,
    DepartmentID INT,
    OfficeLocation NVARCHAR(100),
    HireDate DATE,
    Title NVARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

CREATE TABLE Admin (
    AdminID INT PRIMARY KEY,
    UserID INT,
    RoleDescription NVARCHAR(255),
    Permissions NVARCHAR(255),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Step 5: Modify Other Tables to Reference the Appropriate User Types
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY,
    CourseName NVARCHAR(100) NOT NULL,
    CourseDescription NVARCHAR(255),
    Credits INT,
    DepartmentID INT,
    InstructorID INT,
    Schedule NVARCHAR(100),
    FOREIGN KEY (InstructorID) REFERENCES Faculty(FacultyID),
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

CREATE TABLE Enrollments (
    EnrollmentID INT PRIMARY KEY IDENTITY,
    StudentID INT,
    CourseID INT,
    EnrollmentDate DATE,
    Status NVARCHAR(50), -- e.g., 'Enrolled', 'Completed', 'Dropped'
    Grade DECIMAL(3, 2),
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

-- Step 6: Create the Remaining Tables
CREATE TABLE Assignments (
    AssignmentID INT PRIMARY KEY IDENTITY(1,1),
    CourseID INT,
    AssignmentTitle NVARCHAR(100) NOT NULL,
    AssignmentDescription NVARCHAR(255),
    DueDate DATE,
    MaxPoints INT,
    AssignmentType NVARCHAR(50), -- e.g., 'Homework', 'Project', 'Exam'
    AssignmentFilePath NVARCHAR(255), -- Path to the assignment file
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

CREATE TABLE Submissions (
    SubmissionID INT PRIMARY KEY IDENTITY,
    AssignmentID INT,
    StudentID INT,
    SubmissionDate DATE,
    SubmissionContent NVARCHAR(MAX),
    FilePath NVARCHAR(255), -- If submissions include file uploads
    FOREIGN KEY (AssignmentID) REFERENCES Assignments(AssignmentID),
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
);

CREATE TABLE Grades (
    GradeID INT PRIMARY KEY IDENTITY,
    SubmissionID INT,
    GradeValue DECIMAL(5, 2),
    Feedback NVARCHAR(255),
    GradingDate DATE,
    GraderID INT NULL, -- Nullable, as submissions may not always be graded immediately
    FOREIGN KEY (SubmissionID) REFERENCES Submissions(SubmissionID),
    FOREIGN KEY (GraderID) REFERENCES Faculty(FacultyID)
);

CREATE TABLE StudentResults (
    ResultID INT PRIMARY KEY IDENTITY,
    StudentID INT,
    CourseID INT,
    Grade DECIMAL(5, 2),
    Status NVARCHAR(50), -- e.g., 'Completed', 'In Progress'
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

-- Step 7: Insert Sample Data
-- Insert sample users
INSERT INTO Users (UserName, UserEmail, UserPassword, UserPhone, UserAddress, DateOfBirth, RegistrationDate) VALUES 
('John Doe', 'john.doe@example.com', 'password123', '123-456-7890', '123 Main St', '2000-05-15', '2023-01-10'),   -- Student
('Jane Smith', 'jane.smith@example.com', 'password456', '234-567-8901', '456 Elm St', '1980-08-22', '2022-09-01'), -- Faculty
('Admin User', 'admin@example.com', 'adminpassword', '345-678-9012', '789 Oak St', '1975-03-30', '2021-05-20'),  -- Admin
('Emily Clark', 'emily.clark@example.com', 'password789', '456-789-0123', '321 Pine St', '2001-11-12', '2024-02-15'), -- Student
('Michael Johnson', 'michael.johnson@example.com', 'password101', '567-890-1234', '654 Cedar St', '1979-02-02', '2023-03-01'), -- Faculty
('Susan Lee', 'susan.lee@example.com', 'password102', '678-901-2345', '987 Maple St', '1985-07-17', '2022-11-10'); -- Admin

-- Insert sample majors
INSERT INTO Majors (MajorName) VALUES 
('Computer Science'),
('Information Technology'),
('Business Administration'),
('Electrical Engineering'),
('Mechanical Engineering');

-- Insert sample departments
INSERT INTO Departments (DepartmentName) VALUES 
('Computer Science'),
('Information Technology'),
('Business Administration'),
('Electrical Engineering'),
('Mechanical Engineering');

-- Insert sample students
INSERT INTO Students (UserID, EnrollmentDate, MajorID, YearOfStudy, GPA) VALUES 
(1, '2023-01-10', 1, 2, 3.5), -- John Doe - Computer Science
(4, '2024-02-15', 2, 1, 3.8); -- Emily Clark - Information Technology

-- Insert sample faculty
INSERT INTO Faculty (UserID, DepartmentID, OfficeLocation, HireDate, Title) VALUES 
(2, 1, 'Building A, Room 101', '2010-08-22', 'Associate Professor'), -- Jane Smith - Computer Science
(5, 2, 'Building B, Room 202', '2015-03-01', 'Lecturer');            -- Michael Johnson - Information Technology

-- Insert sample admin
INSERT INTO Admin (UserID, RoleDescription, Permissions) VALUES 
(3, 'System Administrator', 'Full Access'),
(6, 'Course Coordinator', 'Manage Courses');

-- Insert sample courses
INSERT INTO Courses (CourseName, CourseDescription, Credits, DepartmentID, InstructorID, Schedule) VALUES 
('Database Systems', 'Introduction to databases', 3, 1, 2, 'MWF 9:00-10:00 AM'),    -- Jane Smith - Computer Science
('Web Development', 'Learn to build websites', 4, 1, 2, 'TTh 11:00 AM-12:30 PM'),   -- Jane Smith - Computer Science
('Network Security', 'Basics of network security', 3, 2, 5, 'MWF 1:00-2:00 PM'),    -- Michael Johnson - Information Technology
('Data Structures', 'Introduction to data structures', 3, 2, 5, 'TTh 2:00-3:30 PM'); -- Michael Johnson - Information Technology

-- Insert sample enrollments
INSERT INTO Enrollments (StudentID, CourseID, EnrollmentDate, Status, Grade) VALUES 
(1, 1, '2023-01-10', 'Enrolled', NULL), -- John Doe enrolled in Database Systems
(1, 2, '2023-01-11', 'Enrolled', NULL), -- John Doe enrolled in Web Development
(4, 3, '2024-02-16', 'Enrolled', NULL), -- Emily Clark enrolled in Network Security
(4, 4, '2024-02-17', 'Enrolled', NULL); -- Emily Clark enrolled in Data Structures

-- Insert sample assignments
INSERT INTO Assignments (CourseID, AssignmentTitle, AssignmentDescription, DueDate, MaxPoints, AssignmentType, AssignmentFilePath) VALUES 
(1, 'Assignment 1', 'Database schema design', '2024-02-01', 100, 'Homework', 'path/to/database_schema.pdf'),
(1, 'Assignment 2', 'SQL queries', '2024-02-15', 100, 'Homework', 'path/to/sql_queries.pdf'),
(2, 'Assignment 1', 'Build a simple website', '2024-02-15', 100, 'Project', 'path/to/website_project.pdf'),
(3, 'Assignment 1', 'Network analysis report', '2024-03-01', 100, 'Report', 'path/to/network_report.pdf'),
(4, 'Assignment 1', 'Implement linked list', '2024-03-10', 100, 'Homework', 'path/to/linked_list.pdf');

-- Insert sample submissions
INSERT INTO Submissions (AssignmentID, StudentID, SubmissionDate, SubmissionContent, FilePath) VALUES 
(1, 1, '2024-01-30', 'Schema design for LMS', 'path/to/schema_design.pdf'),
(2, 1, '2024-02-10', 'SQL query solutions', 'path/to/sql_queries.sql'),
(3, 1, '2024-02-10', 'Website project', 'path/to/website_project.zip'),
(4, 4, '2024-02-25', 'Network analysis report', 'path/to/network_report.docx'),
(5, 4, '2024-03-05', 'Linked list implementation', 'path/to/linked_list.cpp');

-- Insert sample grades
INSERT INTO Grades (SubmissionID, GradeValue, Feedback, GradingDate, GraderID) VALUES 
(1, 90.5, 'Good work on the schema design.', '2024-02-02', 2),
(2, 85.0, 'Correct SQL queries.', '2024-02-16', 2),
(3, 95.0, 'Excellent website project.', '2024-02-17', 2),
(4, 88.0, 'Detailed network analysis report.', '2024-03-02', 5),
(5, 92.0, 'Well-implemented linked list.', '2024-03-11', 5);

-- Insert sample student results
INSERT INTO StudentResults (StudentID, CourseID, Grade, Status) VALUES 
(1, 1, 90.5, 'Completed'), -- John Doe completed Database Systems
(1, 2, 85.0, 'Completed'), -- John Doe completed Web Development
(4, 3, 88.0, 'In Progress'), -- Emily Clark in progress for Network Security
(4, 4, 92.0, 'In Progress'); -- Emily Clark in progress for Data Structures
