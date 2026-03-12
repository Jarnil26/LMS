import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { User, UpdateProfileDto } from '../../models/auth.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  currentUser: User | null = null;
  profileModel: UpdateProfileDto = { fullName: '', email: '' };
  loading = false;
  successMessage = '';
  errorMessage = '';

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUserValue();
    if (this.currentUser) {
      this.profileModel.fullName = this.currentUser.fullName;
      this.profileModel.email = this.currentUser.email;
    }
  }

  onSubmit(): void {
    this.loading = true;
    this.successMessage = '';
    this.errorMessage = '';

    this.authService.updateProfile(this.profileModel).subscribe({
      next: (response) => {
        if (response.success) {
          this.successMessage = 'Profile updated successfully';
          // Update local user data if needed
        }
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Update failed';
        this.loading = false;
      }
    });
  }
}
