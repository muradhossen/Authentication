import { Component, signal, OnInit } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { AccountService } from './authentication/services/account.service';

@Component({
  standalone: true,
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  protected readonly title = signal('auth-client');

  constructor(private account: AccountService, private router: Router) { }

  ngOnInit(): void {
    // If not authenticated and not already on /auth, redirect to auth module
    const isAuth = this.account.isAuthenticated();
    if (!isAuth && !this.router.url.startsWith('/auth')) {
      // navigate to the authentication module
      this.router.navigate(['/auth']);
    }
  }

  isAuthenticated(): boolean {
    return this.account.isAuthenticated();
  }

  logout() {
    this.account.logout();
    this.router.navigate(['/auth']);
  }
}
