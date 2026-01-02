# Toros Gym Management System

In this project, I have developed a desktop-based gym management application for an imaginary fitness center.
The application is designed as a sample project for the SWE305 course and aims to simulate basic gym management operations.

The system manages gym members, trainers, and workout plans through a simple and user-friendly interface.

This project is intended to demonstrate object-oriented design principles and the use of the MVVM architectural pattern in a WPF desktop application.

## General Design Approach

While designing the application, I followed a simple but realistic approach.
Each class represents a real-life entity in a gym environment and only contains the attributes that are necessary for identifying and managing that entity.

A class should only store information that the application truly needs.
Unnecessary or overly detailed attributes are intentionally avoided to keep the design clean and understandable.

## Core Model Classes

The project includes the following main model classes:

### Member
Represents a gym member.
This class stores identifying and essential information related to members, such as their personal details and membership-related data.

### Trainer
Represents a trainer working at the gym.
It contains basic identifying information required to associate trainers with members or workout plans.

### WorkoutPlan
Represents workout plans assigned to members.
This class stores information about training programs without including unnecessary implementation details.

Each model class is designed to reflect its real-life counterpart while remaining minimal and focused.

## ViewModel Usage

ViewModel classes are used to manage the interaction between the user interface and the model classes.
They are responsible for dynamically displaying data and updating the UI when changes occur.

This separation ensures that the UI logic and the business logic remain independent, following the MVVM design pattern.

## Application Features

- Managing gym members
- Managing trainers
- Assigning and displaying workout plans
- Dynamic UI updates using data binding
- Clear separation of concerns using MVVM

## Technologies Used

- C#
- WPF (Windows Presentation Foundation)
- MVVM Architecture
- .NET
- LINQ
- GitHub for version control

## Demo Video

A demonstration video showing the functionality of the application is available at the following link:

https://drive.google.com/file/d/1hWTm6xECRcH1-9bNfulw9kA4z70WLzu5/view

## Notes

Screenshots of the application interface are included in the GitHub repository as requested.

## Screenshots

### 1. Initial Members List
Initial state of the application showing the list of registered gym members.

![Initial Members](Screenshots/SWE305%20SS1.png)

---

### 2. Member Removal
An existing member is removed from the system. The members list and occupancy rate are updated accordingly.

![Member Removal](Screenshots/SWE305%20SS2.png)

---

### 3. Adding New Members
New gym members are added using the member registration panel on the left side of the application.

![Add Member](Screenshots/SWE305%20SS3.png)

---

### 4. Updating Member Information
An existing member’s information (name, birth date, membership type, trainer, or workout plan) is updated.

![Update Member](Screenshots/SWE305%20SS4.png)

---

### 5. Searching Members by Name
The search box is used to quickly find a specific member by typing the member’s name.

![Search Member](Screenshots/SWE305%20SS5.png)

---

### 6. Searching by Trainer Name
The search functionality is also used to list members by typing a trainer’s name.

![Search Trainer](Screenshots/SWE305%20SS6.png)

---

### 7. Freezing Membership
A member’s membership is frozen. The member becomes inactive and the visual status of the card changes accordingly.

![Freeze Membership](Screenshots/SWE305%20SS7.png)

## Icons Used in the Project

<p align="center">
  <img src="Images/trainer_icon.jpg" width="80" alt="Trainer Icon" />
  &nbsp;&nbsp;&nbsp;
  <img src="Images/user_icon.jpg" width="80" alt="User Icon" />
</p>


## Author

Anıl Keleş  
SWE305 – Toros University
