import { Component } from '@angular/core';

@Component({
  selector: 'app-access-denied',
  template: `
    <div class="access-denied">
      <h1>Access Denied</h1>
      <p>You don't have permission to access this page.</p>
      <a routerLink="/dashboard">Go to Dashboard</a>
    </div>
  `,
  styles: [`
    .access-denied {
      text-align: center;
      padding: 80px 20px;
      color: #0f172a;
      
      h1 {
        font-size: 32px;
        margin: 0 0 16px 0;
      }
      
      p {
        margin: 0 0 24px 0;
        color: #64748b;
      }
      
      a {
        display: inline-block;
        padding: 10px 24px;
        background-color: #2563eb;
        color: white;
        text-decoration: none;
        border-radius: 6px;
        
        &:hover {
          background-color: #1d4ed8;
        }
      }
    }
  `]
})
export class AccessDeniedComponent { }
