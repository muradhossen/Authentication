import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

@Component({
  standalone: false,
  selector: 'app-login',
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
})
export class Login {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private account: AccountService, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  get f() {
    return this.loginForm.controls;
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.account.login(this.loginForm.value).subscribe({
      next: (token) => {
        // token already stored by AccountService - redirect to home after current VM tick
        console.log('Logged in, token:', token);
        Promise.resolve().then(() => this.router.navigate(['/']));
      },
      error: (err) => {
        // TODO: show proper UI error
        console.error('Login failed', err);
      }
    });
  }

}
