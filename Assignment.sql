
--create database PrivateGymDB

-- Tạo bảng Account (Chứa thông tin đăng nhập)
CREATE TABLE Account (
    Email VARCHAR(255) PRIMARY KEY,
    Password VARCHAR(255)
);

-- Tạo bảng Role (Vai trò của tài khoản)
CREATE TABLE Role (
    RoleId INT PRIMARY KEY,
    RoleName VARCHAR(255)
);

-- Tạo bảng TrainingPackages (Gói tập luyện)
CREATE TABLE TrainingPackages (
    PackageID INT PRIMARY KEY,
    PackageType VARCHAR(255),
    Price DECIMAL(10, 2),
    Description TEXT
);

-- Tạo bảng Services (Dịch vụ bổ sung)
CREATE TABLE Services (
    ServiceID INT PRIMARY KEY,
    ServiceType VARCHAR(255),
    Price DECIMAL(10, 2),
    Description TEXT
);

-- Tạo bảng Users (Người dùng hệ thống)
CREATE TABLE Users (
    UserID int PRIMARY KEY ,
    Fullname VARCHAR(255),
    Phone VARCHAR(255),
    Email VARCHAR(255) UNIQUE,
    Address VARCHAR(255),
    DateOfBirth DATE,
    RoleId INT,
    PackageID INT,
    IsActive BIT,
    ServiceID INT,
    FOREIGN KEY (Email) REFERENCES Account(Email),
    FOREIGN KEY (RoleId) REFERENCES Role(RoleId),
    FOREIGN KEY (PackageID) REFERENCES TrainingPackages(PackageID),
    FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID)
);

-- Tạo bảng PersonalTrainers (Huấn luyện viên cá nhân)
CREATE TABLE PersonalTrainers (
    Email VARCHAR(255) PRIMARY KEY,
    Fullname VARCHAR(255),
    Phone VARCHAR(255),
    Address VARCHAR(255),
    DateOfBirth DATE,
    IsActive BIT,
    Expertise TEXT,
    Achievements TEXT,
    FOREIGN KEY (Email) REFERENCES Account(Email) ON DELETE CASCADE
);

-- Tạo bảng User_PT_Booking (Người dùng thuê PT)
CREATE TABLE User_PT_Booking (
    BookingID INT PRIMARY KEY ,
    UserID INT,
    PT_Email VARCHAR(255),
    BookingDate DATE,
    Status VARCHAR(50) DEFAULT 'Pending',  -- (Pending, Confirmed, Cancelled)
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (PT_Email) REFERENCES PersonalTrainers(Email) ON DELETE CASCADE
);

CREATE TABLE TrainingSchedule (
    ScheduleID INT PRIMARY KEY,
    UserEmail VARCHAR(255),
    PT_Email VARCHAR(255),
    TrainingDate DATE,
    StartTime TIME,
    Duration INT,
    Notes TEXT,
    BookingID INT,
    FOREIGN KEY (UserEmail) REFERENCES Users(Email) ON DELETE CASCADE,
    FOREIGN KEY (PT_Email) REFERENCES PersonalTrainers(Email) ON DELETE CASCADE,
    FOREIGN KEY (BookingID) REFERENCES User_PT_Booking(BookingID) ON DELETE NO ACTION  -- Change applied here
);

-- Tạo bảng Discounts (Mã giảm giá)
CREATE TABLE Discounts (
    DiscountID INT PRIMARY KEY ,
    DiscountCode VARCHAR(255),
    DiscountPercent DECIMAL(5, 2),
    ExpiryDate DATE
);

-- Tạo bảng Invoices (Hóa đơn)
CREATE TABLE Invoices (
    InvoiceID INT PRIMARY KEY ,
    UserPhone VARCHAR(255),
    PaymentDate DATE,
    DiscountID INT,
    TotalAmount DECIMAL(10, 2),
    UserID INT,
    FOREIGN KEY (DiscountID) REFERENCES Discounts(DiscountID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- ============================================
-- 🌟 DỮ LIỆU MẪU (INSERT DATA)
-- ============================================
-- Step 1: Insert data into the Account table
INSERT INTO Account (Email, Password) VALUES 
('user1@example.com', 'password123'),
('manager@example.com', 'password123'),
('admin@example.com', 'password123'),
('pt1@example.com', 'password123'),
('pt2@example.com', 'password123');

-- Step 2: Insert data into the Role table
INSERT INTO Role (RoleId, RoleName) VALUES 
(1, 'User'),
(2, 'PT'),
(3, 'Admin'),
(4, 'Manager');

-- Tạo bảng Room (Phòng tập)
CREATE TABLE Room (
    RoomID INT PRIMARY KEY ,
    RoomName nVARCHAR(255) NOT NULL,
    Capacity INT NOT NULL,
    IsAvailable BIT DEFAULT 1 -- 1: Còn trống, 0: Đã kín
);
-- Tạo bảng Room_Booking (Đặt phòng tập)
CREATE TABLE Room_Booking (
    BookingID INT PRIMARY KEY ,
    UserID INT NOT NULL,       -- Người dùng đặt phòng
    PT_Email VARCHAR(255),     -- PT được gán vào phòng (có thể NULL nếu không có PT)
    RoomID INT NOT NULL,       -- Phòng tập được đặt
    TrainingDate DATE NOT NULL,-- Ngày tập
    StartTime TIME NOT NULL,   -- Giờ bắt đầu tập
    Duration INT NOT NULL,     -- Thời lượng tập (phút)
    Status VARCHAR(50) DEFAULT 'Pending',  -- Trạng thái (Pending, Confirmed, Cancelled)
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (PT_Email) REFERENCES PersonalTrainers(Email) ON DELETE SET NULL,
    FOREIGN KEY (RoomID) REFERENCES Room(RoomID) ON DELETE CASCADE
);

-- Step 3: Insert data into the TrainingPackages table
INSERT INTO TrainingPackages (PackageID, PackageType, Price, Description) VALUES 
(1, 'Daily Pass', 5.00, 'Gói tập luyện theo ngày'),
(2, 'Monthly Package', 50.00, 'Gói tập luyện 1 tháng'),
(3, 'Half-Year Package', 250.00, 'Gói tập luyện 6 tháng'),
(4, 'Annual Package', 450.00, 'Gói tập luyện 1 năm');

-- Step 4: Insert data into the Services table
INSERT INTO Services (ServiceID, ServiceType, Price, Description) VALUES
(1, 'Personal Training', 20.00, 'Dịch vụ huấn luyện cá nhân'),
(2, 'Group Class', 10.00, 'Dịch vụ lớp nhóm'),
(3, 'Massage', 30.00, 'Dịch vụ massage thư giãn'),
(4, 'Nutrition', 15.00, 'Dịch vụ tư vấn dinh dưỡng'),
(5, 'Yoga', 25.00, 'Dịch vụ Yoga');

-- Step 5: Insert data into the Users table
INSERT INTO Users (UserID, Fullname, Phone, Email, Address, DateOfBirth, RoleId, PackageID, IsActive, ServiceID) VALUES 
(1, 'John Doe', '123456789', 'user1@example.com', '123 Main St', '1990-05-15', 1, 1, 1, 1),
(2, 'Jane Smith', '987654321', 'manager@example.com', '456 Elm St', '1985-09-25', 4, 2, 1, 3),
(3, 'Alice Johnson', '555888777', 'admin@example.com', '789 Pine St', '1992-12-10', 3, NULL, 1, 5),
(4, 'Nguyen', '555888777', 'pt1@example.com', '789 Pine St', '1992-12-10', 2, NULL, 1, 4),
(5, 'Hai', '4333333333333', 'pt2@example.com', '789 Pine St', '1992-12-10', 2, NULL, 1, 5);

-- Step 6: Insert data into the PersonalTrainers table
INSERT INTO PersonalTrainers (Email, Fullname, Phone, Address, DateOfBirth, IsActive, Expertise, Achievements) 
VALUES 
('pt1@example.com', 'Michael Brown', '333222111', '101 Fitness St', '1980-07-20', 1, 'Strength Training, Cardio', 'Certified Fitness Trainer'),
('pt2@example.com', 'Sarah Williams', '444555666', '202 Gym Ave', '1988-04-12', 1, 'Yoga, Pilates', 'Yoga Instructor Certification');

-- Step 7: Insert data into the User_PT_Booking table
INSERT INTO User_PT_Booking (BookingID, UserID, PT_Email, BookingDate, Status) VALUES 
(1, 1, 'pt1@example.com', '2025-02-28', 'Confirmed'),
(2, 1, 'pt2@example.com', '2025-03-02', 'Pending');

-- Step 8: Insert data into the TrainingSchedule table
INSERT INTO TrainingSchedule (ScheduleID, UserEmail, PT_Email, TrainingDate, StartTime, Duration, Notes, BookingID) VALUES 
(1, 'user1@example.com', 'pt1@example.com', '2025-03-01', '10:00:00', 60, 'Buổi tập Cardio nâng cao', 1),
(2, 'user1@example.com', 'pt2@example.com', '2025-03-05', '15:00:00', 45, 'Yoga thư giãn', 2);



-- Thêm dữ liệu mẫu vào Room
INSERT INTO Room (RoomID,RoomName, Capacity, IsAvailable) VALUES 
(1,'Phòng Cardio', 10, 1),
(2,'Phòng Yoga', 15, 1),
(3,'Phòng Gym Cơ Bản', 20, 1),
(4,'Phòng Tập Nâng Cao', 8, 1);
-- Thêm dữ liệu mẫu vào Room (Private Gym)
INSERT INTO Room (RoomID, RoomName, Capacity, IsAvailable) VALUES 
(5, 'Private Strength Training', 5, 1),
(6, 'VIP Personal Coaching', 3, 1),
(7, 'Elite Performance Studio', 4, 1),
(8, 'Pro Athlete Zone', 6, 1),
(9, 'Exclusive Wellness Suite', 2, 1);
-- Thêm dữ liệu mẫu vào Room_Booking
INSERT INTO Room_Booking (BookingID,UserID, PT_Email, RoomID, TrainingDate, StartTime, Duration, Status) VALUES 
(1,1, 'pt1@example.com', 1, '2025-03-10', '08:00:00', 60, 'Confirmed'),
(2,2, NULL, 2, '2025-03-11', '10:00:00', 45, 'Pending'),
(3,3, 'pt2@example.com', 3, '2025-03-12', '14:00:00', 90, 'Confirmed');
CREATE TABLE Time_Slots (
    SlotID INT PRIMARY KEY ,
    StartTime TIME NOT NULL,  -- Giờ bắt đầu
    EndTime TIME NOT NULL     -- Giờ kết thúc
);
INSERT INTO Time_Slots (SlotID,StartTime, EndTime) VALUES
(1,'06:00:00', '08:00:00'),
(2,'08:30:00', '10:30:00'),
(3,'14:00:00', '16:00:00'),
(4,'16:30:00', '18:30:00'),
(5,'19:00:00', '21:00:00');
CREATE TABLE Room_Schedule (
    ScheduleID INT PRIMARY KEY ,
    RoomID INT NOT NULL,         
    TrainingDate DATE NOT NULL,   -- Ngày tập
    SlotID INT NOT NULL,          -- Tham chiếu đến khung giờ cố định
    IsBooked BIT DEFAULT 0,       -- 0: Còn trống, 1: Đã đặt
    FOREIGN KEY (RoomID) REFERENCES Room(RoomID) ON DELETE CASCADE,
    FOREIGN KEY (SlotID) REFERENCES Time_Slots(SlotID) ON DELETE CASCADE
);
CREATE TABLE Trainer_Availability (
    AvailabilityID INT PRIMARY KEY ,
    PT_Email VARCHAR(255) NOT NULL,   -- Liên kết với Email của PT
    TrainingDate DATE NOT NULL,       -- Ngày tập
    SlotID INT NOT NULL,              -- Tham chiếu đến khung giờ cố định
    IsAvailable BIT DEFAULT 1,        -- 1: Có thể nhận lớp, 0: Không thể nhận lớp
    FOREIGN KEY (PT_Email) REFERENCES PersonalTrainers(Email) ON DELETE CASCADE,
    FOREIGN KEY (SlotID) REFERENCES Time_Slots(SlotID) ON DELETE CASCADE
);
INSERT INTO Trainer_Availability (AvailabilityID,PT_Email, TrainingDate, SlotID, IsAvailable) VALUES
-- PT1 rảnh toàn bộ ca trong ngày
(1,'pt1@example.com', '2025-03-15', 1, 1), -- 6:00 - 8:00
(2,'pt1@example.com', '2025-03-15', 2, 1), -- 8:30 - 10:30
(3,'pt1@example.com', '2025-03-15', 3, 1), -- 14:00 - 16:00
(4,'pt1@example.com', '2025-03-15', 4, 1), -- 16:30 - 18:30
(5,'pt1@example.com', '2025-03-15', 5, 1), -- 19:00 - 21:00

-- PT2 rảnh toàn bộ ca trong ngày
(6,'pt2@example.com', '2025-03-15', 1, 1), -- 6:00 - 8:00
(7,'pt2@example.com', '2025-03-15', 2, 1), -- 8:30 - 10:30
(8,'pt2@example.com', '2025-03-15', 3, 1), -- 14:00 - 16:00
(9,'pt2@example.com', '2025-03-15', 4, 1), -- 16:30 - 18:30
(10,'pt2@example.com', '2025-03-15', 5, 1); -- 19:00 - 21:00

ALTER TABLE Users 
ADD Img VARCHAR(255); -- Thêm cột ảnh đại diện cho người dùng

ALTER TABLE Room 
ADD Description varchar(255)
ALTER TABLE Room -- Mô tả về phòng tập
ADD [Location] VARCHAR(255) 
ALTER TABLE Room -- Vị trí phòng tập (đã đặt trong dấu ngoặc vuông)
ADD Img VARCHAR(255)  -- Hình ảnh phòng tập

UPDATE Room 
SET Description = 'A dedicated cardio room with treadmills, bikes, and elliptical machines. Designed to improve endurance and burn calories in a comfortable space.',
    [Location] = 'Floor 1 - Zone A'
WHERE RoomID = 1;

UPDATE Room 
SET Description = 'A peaceful yoga studio with ambient lighting, meditation mats, and a relaxing environment. Perfect for flexibility, balance, and mindfulness.',
    [Location] = 'Floor 2 - Zone B'
WHERE RoomID = 2;

UPDATE Room 
SET Description = 'A strength training facility with weight machines, free weights, and squat racks. Designed for muscle building, endurance, and fitness improvement.',
    [Location] = 'Floor 3 - Zone C'
WHERE RoomID = 5;

UPDATE Room 
SET Description = 'A private VIP coaching room for one-on-one training with elite trainers. Equipped with smart mirrors, biometric tracking, and premium fitness tools.',
    [Location] = 'Floor 4 - Zone D'
WHERE RoomID = 6;

UPDATE Room 
SET Description = 'An elite performance studio with advanced fitness tech, motion tracking, and specialized training. Designed for high-performance athletes.',
    [Location] = 'Floor 5 - Zone E'
WHERE RoomID = 7;

UPDATE Room 
SET Description = 'A specialized training area for professional athletes with sprint tracks, Olympic-standard equipment, and recovery stations for peak performance.',
    [Location] = 'Floor 6 - Zone F'
WHERE RoomID = 8;

UPDATE Room 
SET Description = 'A luxury wellness suite with hydrotherapy, saunas, massage therapy, and stress relief programs. Perfect for relaxation, detox, and recovery.',
    [Location] = 'Floor 7 - Zone G'
WHERE RoomID = 9;


ALTER TABLE Room_Schedule
ADD UserID INT;

-- Cập nhật liên kết với bảng Users
ALTER TABLE Room_Schedule
ADD FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE;

DROP TABLE Room_Booking;
