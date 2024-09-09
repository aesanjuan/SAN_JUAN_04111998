import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ShortClipsService } from '../../../services/short-clips/short-clips.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-stream',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
  ],
  templateUrl: './stream.component.html',
  styleUrl: './stream.component.css'
})
export class StreamComponent implements OnInit, OnDestroy {
  @ViewChild("videoPlayer", { static: false })
  videoPlayer!: ElementRef;
  
  videoId: string | null;
  title: string = '';
  description: string = '';
  category: string = '';
  uploadDateTime: Date = new Date();
  videoContentType: string = '';
  videoSourceLink: string = '';

  isLoaded: boolean = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private shortClipsService: ShortClipsService,
  ) {
    this.videoId = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {
    if (this.videoId == null) {
      this.router.navigate(['/home']);
    } else {
      this.shortClipsService.getShortClip(Number(this.videoId)).subscribe((response: any) => {
        this.title = response['title'];
        this.description = response['description'];
        this.uploadDateTime = response['uploadDateTime'];

        // play video
        this.playVideo(response['id'], response['videoContentType']);
      });

      this.isLoaded = true;
    }
  }

  playVideo(id: string, contentType: string): void {
    // play video
    this.videoSourceLink = `http://localhost:5252/api/ShortClips/Stream?id=${id}`;
    this.videoPlayer.nativeElement.src = this.videoSourceLink;
    this.videoPlayer.nativeElement.type = contentType;
    this.videoPlayer.nativeElement.play();
  }

  ngOnDestroy(): void { }
}
