import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

@Component({
  standalone: false,
  selector: 'app-registration',
  templateUrl: './registration.html',
  styleUrls: ['./registration.css'],
})
export class Registration {
  registrationForm: FormGroup;

  constructor(private fb: FormBuilder, private account: AccountService, private router: Router) {
    this.registrationForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: [''],
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      gender: [''],
      address: [''],
      dob: [''],
    });
  }

  get f() {
  return this.registrationForm.controls;
  }

  onSubmit(): void {
    if (this.registrationForm.invalid) {
      this.registrationForm.markAllAsTouched();
      return;
    }

    this.account.register(this.registrationForm.value).subscribe({
      next: (token) => {
        console.log('Registered, token:', token);
        // token already stored by AccountService. Redirect to app root or login
        Promise.resolve().then(() => this.router.navigate(['/']));
      },
      error: (err) => {
        console.error('Registration failed', err);
      }
    });
  }

}
