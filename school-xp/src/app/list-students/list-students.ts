import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

interface Student {
  id: string;
  initials: string;
  name: string;
  grade: string;
  route: string;
  attendance: 'Present' | 'Absent' | 'Late';
  classTeacher: string;
  avatarClass: string;
}

@Component({
  imports: [FormsModule, RouterLink],
  selector: 'app-list-students',
  styleUrl: './list-students.css',
  templateUrl: './list-students.html',
})
export class ListStudents {
  protected searchTerm = '';
  protected selectedGrade = '';
  protected selectedRoute = '';
  protected selectedAttendance = '';

  protected readonly students: Student[] = [
    {
      id: 'maya-chen',
      initials: 'MC',
      name: 'Maya Chen',
      grade: 'Grade 6',
      route: 'Route A',
      attendance: 'Present',
      classTeacher: 'James Lee',
      avatarClass: 'avatar-coral',
    },
    {
      id: 'noah-williams',
      initials: 'NW',
      name: 'Noah Williams',
      grade: 'Grade 7',
      route: 'Route B',
      attendance: 'Late',
      classTeacher: 'Sofia Patel',
      avatarClass: 'avatar-mint',
    },
    {
      id: 'aria-johnson',
      initials: 'AJ',
      name: 'Aria Johnson',
      grade: 'Grade 8',
      route: 'Route A',
      attendance: 'Present',
      classTeacher: 'Oliver Bennett',
      avatarClass: 'avatar-gold',
    },
    {
      id: 'ethan-walker',
      initials: 'EW',
      name: 'Ethan Walker',
      grade: 'Grade 6',
      route: 'Route C',
      attendance: 'Absent',
      classTeacher: 'James Lee',
      avatarClass: 'avatar-blue',
    },
    {
      id: 'lina-patel',
      initials: 'LP',
      name: 'Lina Patel',
      grade: 'Grade 7',
      route: 'Route C',
      attendance: 'Present',
      classTeacher: 'Sofia Patel',
      avatarClass: 'avatar-lilac',
    },
  ];

  protected filteredStudents: Student[] = [];
  protected readonly grades = ['Grade 6', 'Grade 7', 'Grade 8'];
  protected readonly routes = ['Route A', 'Route B', 'Route C'];
  protected readonly attendanceOptions: Student['attendance'][] = ['Present', 'Late', 'Absent'];

  protected searchStudents(): void {
    const search = this.searchTerm.trim().toLowerCase();
    this.filteredStudents = this.students.filter((student) => {
      const matchesSearch = !search || [student.name, student.classTeacher, student.grade, student.route]
        .some((value) => value.toLowerCase().includes(search));
      const matchesGrade = !this.selectedGrade || student.grade === this.selectedGrade;
      const matchesRoute = !this.selectedRoute || student.route === this.selectedRoute;
      const matchesAttendance = !this.selectedAttendance || student.attendance === this.selectedAttendance;

      return matchesSearch && matchesGrade && matchesRoute && matchesAttendance;
    });
  }

  protected clearFilters(): void {
    this.searchTerm = '';
    this.selectedGrade = '';
    this.selectedRoute = '';
    this.selectedAttendance = '';
    this.filteredStudents = [];
  }
}
