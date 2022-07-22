import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter<boolean>();
  model: any = {};

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  register() {
    this.accountService.register(this.model).subscribe(
      (response) => {
        console.log(response);
        console.log('Successfull you can login now');
        this.cancel();
      },
      (error) => {
        console.error(error);
      }
    );
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
