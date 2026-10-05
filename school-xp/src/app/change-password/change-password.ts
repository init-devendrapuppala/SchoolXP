import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  imports: [CommonModule, FormsModule, RouterLink],
  selector: 'app-change-password',
  styleUrl: './change-password.css',
  templateUrl: './change-password.html',
})
export class ChangePassword {
  email: string = '';
  isEmailSent: boolean = false;
  otp: string = '';
  isOtpValid: boolean = false;
  newPassword: string = '';
  confirmPassword: string = '';
  isPasswordUpdated: boolean = false;

  sendPasswordResetEmail(){
    if(this.email){
      // Logic to send password reset email
      this.isEmailSent = true;
      console.log(`Password reset email sent to ${this.email}`);
    } else {
      console.log('Please enter a valid email address.');
    }
  }

  verifyOtp(){
    if(this.otp){
      // Logic to verify OTP
      this.isOtpValid = true;
      console.log(`OTP verified for email: ${this.email}, OTP: ${this.otp}`);
    } else {
      console.log('Please enter the OTP.');
    }
  }

  updatePassword(){
    if(this.newPassword && this.confirmPassword){
      if(this.newPassword === this.confirmPassword){
        // Logic to update password
        this.isPasswordUpdated = true;
        console.log(`Password updated for email: ${this.email}`);
      } else {
        console.log('Passwords do not match.');
      }
    } else {
      console.log('Please fill in all password fields.');
    }
  }
}
