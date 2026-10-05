import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

interface StaffMember {
  id: string;
  initials: string;
  name: string;
  email: string;
  type: 'Academic' | 'Administrative';
  subjectOrDepartment: string;
  avatarClass: string;
  typeClass: string;
}

@Component({
  imports: [RouterLink],
  selector: 'app-list-staff',
  styleUrl: './list-staff.css',
  templateUrl: './list-staff.html',
})
export class ListStaff {
  protected readonly staffMembers: StaffMember[] = [
    {
      id: 'amelia-morgan',
      initials: 'AM',
      name: 'Amelia Morgan',
      email: 'amelia.morgan@northbridge.edu',
      type: 'Administrative',
      subjectOrDepartment: 'Leadership',
      avatarClass: 'avatar-coral',
      typeClass: 'type-administrative',
    },
    {
      id: 'james-lee',
      initials: 'JL',
      name: 'James Lee',
      email: 'james.lee@northbridge.edu',
      type: 'Academic',
      subjectOrDepartment: 'Mathematics',
      avatarClass: 'avatar-mint',
      typeClass: 'type-academic',
    },
    {
      id: 'sofia-patel',
      initials: 'SP',
      name: 'Sofia Patel',
      email: 'sofia.patel@northbridge.edu',
      type: 'Administrative',
      subjectOrDepartment: 'Administration',
      avatarClass: 'avatar-gold',
      typeClass: 'type-administrative',
    },
    {
      id: 'oliver-bennett',
      initials: 'OB',
      name: 'Oliver Bennett',
      email: 'oliver.bennett@northbridge.edu',
      type: 'Academic',
      subjectOrDepartment: 'Science',
      avatarClass: 'avatar-blue',
      typeClass: 'type-academic',
    },
    {
      id: 'eva-collins',
      initials: 'EC',
      name: 'Eva Collins',
      email: 'eva.collins@northbridge.edu',
      type: 'Administrative',
      subjectOrDepartment: 'Student support',
      avatarClass: 'avatar-lilac',
      typeClass: 'type-administrative',
    },
  ];
}
