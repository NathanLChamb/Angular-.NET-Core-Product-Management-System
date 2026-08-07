import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../auth/auth-service';
import { ProductFilterService } from '../../../features/product/product-filter-service';

@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.css',
})
export class NavBar {
  authService = inject(AuthService);
  filterService = inject(ProductFilterService);

  resetSearch() {
    this.filterService.reset();
  }

  logout(): void {
    this.authService.logout();
  }
}
