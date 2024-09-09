import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { HomeComponent } from './components/home/home.component';
import { UploadFormComponent } from './components/upload-form/upload-form.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterModule,
    HomeComponent,
    UploadFormComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent {
  title = 'short-clips-web-ui';
}
