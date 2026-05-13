import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBar } from '../nav-bar/nav-bar';

@Component({
  selector: 'app-page-layout',
  imports: [RouterOutlet, NavBar],
  templateUrl: './page-layout.html',
  styleUrl: './page-layout.css',
})
export class PageLayout {

}
