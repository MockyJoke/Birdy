import { TestBed, inject } from '@angular/core/testing';

import { PhotoManagerService } from './photo-manager.service';

describe('PhotoManagerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PhotoManagerService]
    });
  });

  it('should be created', inject([PhotoManagerService], (service: PhotoManagerService) => {
    expect(service).toBeTruthy();
  }));
});
