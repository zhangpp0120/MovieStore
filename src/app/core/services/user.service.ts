import { Purchase } from './../../shared/models/purchase';
import { ApiService } from './api.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private apiService: ApiService) { }

  PurchaseMovie(purchase: Purchase){
    return this.apiService.create('/user/purchase', purchase)
  }














}
