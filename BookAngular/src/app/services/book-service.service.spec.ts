/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { BookServiceService } from './book-service.service';

describe('Service: BookService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BookServiceService]
    });
  });

  it('should ...', inject([BookServiceService], (service: BookServiceService) => {
    expect(service).toBeTruthy();
  }));
});
