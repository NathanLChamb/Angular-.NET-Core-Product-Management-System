import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PageLayout } from './core/layout/page-layout/page-layout';

@Component({
  selector: 'app-root',
  imports: [PageLayout],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('eCommercePractice5');
}
