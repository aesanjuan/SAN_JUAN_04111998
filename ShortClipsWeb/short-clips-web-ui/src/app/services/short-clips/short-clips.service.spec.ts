import { TestBed } from '@angular/core/testing';

import { ShortClipsService } from './short-clips.service';

describe('ShortClipsService', () => {
  let service: ShortClipsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShortClipsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
