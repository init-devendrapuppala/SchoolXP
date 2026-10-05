import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  imports: [FormsModule, RouterLink],
  selector: 'app-add-school-admin',
  styleUrl: './add-school-admin.css',
  templateUrl: './add-school-admin.html',
})
export class AddSchoolAdmin {
  submitSchoolAdmin(): void {}

  cancel(): void {}
}
