import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ShortClipsService } from '../../services/short-clips/short-clips.service';
import { Video } from '../../models/video.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})

export class HomeComponent implements OnInit, OnDestroy {
  @ViewChild("imageDisplay", { static: false })
  imageDisplay!: ElementRef;

  videoList: Video[] = [];
  imgSourceLink: string = '';

  constructor(
    private shortClipsService: ShortClipsService,
  ) {}

  ngOnInit(): void {
    this.shortClipsService.getAllShortClips().subscribe((response: any) => {
      this.videoList = response;
      this.videoList.forEach(video => {
        this.imgSourceLink = `http://localhost:5252/api/ShortClips/Image?id=${video.id}`;
        video.imgSourceLink = this.imgSourceLink;
      })
    })
  }

  ngOnDestroy(): void {    
  }
}
