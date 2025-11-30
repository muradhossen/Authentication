import { Injectable, signal, WritableSignal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService {

  private readonly storageKey = 'auth_token';
  private readonly baseUrl = 'https://localhost:7257/api/Account';

  // reactive signal representing authentication state
  readonly authenticated: WritableSignal<boolean> = signal<boolean>(!!this.getToken());

  constructor(private http: HttpClient) { }

  /**
   * Register a new account. Expects the backend to return { token: string } or { accessToken: string }.
   */
  register(payload: { firstName: string; lastName?: string; username: string; gender?: string; address?: string; dob?: string }): Observable<string> {
    const url = `${this.baseUrl}/register`;
    return this.http.post<any>(url, payload).pipe(
      map((res) => res?.token ?? res?.accessToken ?? ''),
      tap((token) => {
        if (token) this.setToken(token);
      })
    );
  }

  /**
   * Login using credentials. Backend returns { token: string } or { accessToken: string }.
   */
  login(payload: { username: string; password: string }): Observable<string> {
    const url = `${this.baseUrl}/login`;
    return this.http.post<any>(url, payload).pipe(
      map((res) => res?.token ?? res?.accessToken ?? ''),
      tap((token) => {
        if (token) this.setToken(token);
      })
    );
  }

  logout() {
    this.clearToken();
  }

  setToken(token: string) {
    localStorage.setItem(this.storageKey, token);
    this.authenticated.set(true);
  }

  getToken(): string | null {
    return localStorage.getItem(this.storageKey);
  }

  clearToken() {
    localStorage.removeItem(this.storageKey);
    this.authenticated.set(false);
  }

  isAuthenticated(): boolean {
    return this.authenticated();
  }


}
