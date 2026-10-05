import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { confirmSignUp, signIn, signUp } from 'aws-amplify/auth';

@Component({
  imports: [FormsModule, RouterLink],
  selector: 'app-login',
  styleUrl: './login.css',
  templateUrl: './login.html',
})
export class Login {
  mode: 'login' | 'signup' | 'confirm' = 'login';
  email = '';
  password = '';
  firstName = '';
  lastName = '';
  confirmationCode = '';
  errorMessage = '';
  statusMessage = '';
  isSubmitting = false;

  constructor(private readonly router: Router) {}

  showLogin(): void {
    this.mode = 'login';
    this.errorMessage = '';
    this.statusMessage = '';
  }

  showSignUp(): void {
    this.mode = 'signup';
    this.errorMessage = '';
    this.statusMessage = '';
  }

  async login(): Promise<void> {
    this.errorMessage = '';
    this.statusMessage = '';
    this.isSubmitting = true;

    try {
      const result = await signIn({
        username: this.email.trim(),
        password: this.password,
      });

      if (result.nextStep.signInStep === 'DONE') {
        await this.router.navigateByUrl('/auth/callback');
        return;
      }

      if (result.nextStep.signInStep === 'CONFIRM_SIGN_UP') {
        this.mode = 'confirm';
        this.statusMessage = 'Confirm your email address to finish creating your account.';
        return;
      }

      this.errorMessage = `This account needs an additional sign-in step (${result.nextStep.signInStep}), which is not available yet.`;
    } catch (error: unknown) {
      this.errorMessage = this.getErrorMessage(error, 'Unable to sign in. Check your details and try again.');
    } finally {
      this.isSubmitting = false;
    }
  }

  async signUp(): Promise<void> {
    this.errorMessage = '';
    this.statusMessage = '';
    this.isSubmitting = true;

    try {
      const result = await signUp({
        username: this.email.trim(),
        password: this.password,
        options: {
          userAttributes: {
            email: this.email.trim(),
            given_name: this.firstName.trim(),
            family_name: this.lastName.trim(),
          },
        },
      });

      if (result.nextStep.signUpStep === 'CONFIRM_SIGN_UP') {
        this.mode = 'confirm';
        this.statusMessage = 'Enter the confirmation code sent to your email address.';
        return;
      }

      this.mode = 'login';
      this.password = '';
      this.statusMessage = 'Your account has been created. Sign in to continue.';
    } catch (error: unknown) {
      this.errorMessage = this.getErrorMessage(error, 'Unable to create your account. Please try again.');
    } finally {
      this.isSubmitting = false;
    }
  }

  async confirmAccount(): Promise<void> {
    this.errorMessage = '';
    this.statusMessage = '';
    this.isSubmitting = true;

    try {
      await confirmSignUp({
        username: this.email.trim(),
        confirmationCode: this.confirmationCode.trim(),
      });
      this.mode = 'login';
      this.password = '';
      this.confirmationCode = '';
      this.statusMessage = 'Your account is confirmed. Sign in to continue.';
    } catch (error: unknown) {
      this.errorMessage = this.getErrorMessage(error, 'Unable to confirm your account. Check the code and try again.');
    } finally {
      this.isSubmitting = false;
    }
  }

  private getErrorMessage(error: unknown, fallback: string): string {
    return error instanceof Error && error.message ? error.message : fallback;
  }
}
