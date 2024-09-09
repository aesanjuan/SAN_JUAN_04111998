import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})

export class CategoriesService {
  private apiUrl: string = environment.apiUrl;

  constructor(
    private http: HttpClient
  ) { }

  /**
   * Triggers a call to an endpoint that retrieves the list of video categories.
   */
  getAllCategories() {
    return this.http.get(this.apiUrl + 'api/Categories/GetAllCategories');
  }
}
