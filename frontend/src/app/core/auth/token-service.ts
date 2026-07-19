import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

interface JwtPayload {
  sub?: string;
  email?: string;
  name?: string;
  role?: string;
  exp?: number;
}

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private readonly tokenKey = 'auth_token';

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  removeToken(): void {
    localStorage.removeItem(this.tokenKey);
  }

  hasValidToken(): boolean {

  const token = this.getToken();

  if (!token) {
    return false;
  }

  try {
    const decoded = jwtDecode<JwtPayload>(token);

    if (!decoded.exp) {
      return false;
    }

    return decoded.exp * 1000 > Date.now();

  } catch {
    return false;
  }
}

  decodeToken(): JwtPayload | null {

    const token = this.getToken();

    if (!token) {
      return null;
    }

    try {
      return jwtDecode<JwtPayload>(token);
    }
    catch {
      return null;
    }
  }
}
