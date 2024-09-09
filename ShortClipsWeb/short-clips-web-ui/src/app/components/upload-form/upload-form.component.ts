import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { ShortClipsService } from '../../services/short-clips/short-clips.service';
import { Video } from '../../models/video.model';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Category } from '../../models/category.model';
import { CategoriesService } from '../../services/categories/categories.service';

@Component({
  selector: 'app-upload-form',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './upload-form.component.html',
  styleUrl: './upload-form.component.css'
})

export class UploadFormComponent implements OnInit, OnDestroy {
  video: Video = new Video();
  categoryOptions: Category[] = [];

  uploadForm: FormGroup = new FormGroup({
    title: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(160)]),
    description: new FormControl ('', [Validators.required, Validators.minLength(1), Validators.maxLength(160)]),
    category: new FormControl([Validators.required]),
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required])
  });

  constructor(
    private router: Router,
    private categoriesService: CategoriesService,
    private shortClipsService: ShortClipsService,
  ) { }

  ngOnInit(): void {
    // get category options
    this.categoriesService.getAllCategories().subscribe((response: any) => {
      this.categoryOptions = response;
    })
  }

  // Handler for file input change
  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.uploadForm.patchValue({
        fileSource: file
      });
    }
  }

  onSubmit() {
    const formData = new FormData();
    const fileSourceValue = this.uploadForm.get('fileSource')?.value;
      if (fileSourceValue !== null && fileSourceValue !== undefined) {
        formData.append('file', fileSourceValue);
      }

    if (this.uploadForm.valid) {  
      // prepare request
      let video = new Video();
      video.title = this.uploadForm.value.title;
      video.description = this.uploadForm.value.description;
      video.category = this.uploadForm.value.category;
  
      this.shortClipsService.uploadShortClip(formData, video.title, video.description, video.category)
        .subscribe((response: any) => {
          // notify the user
          alert("Video uploaded successfully.");
  
          // redirect to streaming page when uploaded successfully
          this.router.navigate(['/stream/' + response['Id']]);
        });
    }    
  }

  ngOnDestroy(): void {
  }
}
