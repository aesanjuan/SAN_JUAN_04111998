import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})

export class ShortClipsService {
  private apiUrl: string = environment.apiUrl;

  constructor(
    private http: HttpClient
  ) { }

  /**
   * Triggers a call to an endpoint that uploads the video.
   */
  uploadShortClip(videoFileToUpload: FormData, title: string, description: string, category: number) {
    let params = new HttpParams();
    params = params.append('title', title);
    params = params.append('description', description);
    params = params.append('categoryId', category);

    return this.http.post(this.apiUrl + 'api/ShortClips/UploadShortClip', videoFileToUpload, { params: params });
  }

  /**
   * Triggers a call to an endpoint that retrieves the list of short clips or videos.
   */
  getAllShortClips() {
    return this.http.get(this.apiUrl + 'api/ShortClips/GetAllShortClips');
  }

  /**
   * Triggers a call to an endpoint that retrieves the video and its details.
   */
  getShortClip(videoId: number) {
    let params = new HttpParams();
    params = params.append('id', videoId);

    return this.http.get(this.apiUrl + 'api/ShortClips/GetShortClip', { params: params });
  }

  /**
   * Triggers a call to an endpoint that updates the details of video.
   */
  updateShortClip(title: string, description: string, category: number) {
    let params = new HttpParams();
    params = params.append('title', title);
    params = params.append('description', description);
    params = params.append('categoryId', category);

    return this.http.get(this.apiUrl + 'api/ShortClips/UpdateShortClip', { params: params });
  }

  /**
   * Triggers a call to an endpoint that deletes the short clip or video.
   */
  deleteShortClip(videoId: number) {
    let params = new HttpParams();
    params = params.append('id', videoId);

    return this.http.get(this.apiUrl + 'api/ShortClips/DeleteShortClip', { params: params });
  }
}
