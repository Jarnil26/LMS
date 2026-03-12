import { Component, Input, Output, EventEmitter } from '@angular/core';
import { User } from '../../models/auth.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
  @Input() currentUser: User | null = null;
  @Output() logout = new EventEmitter<void>();

  constructor(private router: Router) { }

  isActive(route: string): boolean {
    return this.router.url.includes(route);
  }

  onLogout() {
    this.logout.emit();
    this.router.navigate(['/login']);
  }

  navigateTo(route: string) {
    this.router.navigate([route]);
  }
}
