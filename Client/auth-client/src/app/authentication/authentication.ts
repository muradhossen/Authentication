import { Component } from '@angular/core';
import { RouterOutlet } from "@angular/router";

@Component({
  standalone: true,
  selector: 'app-authentication',
  templateUrl: './authentication.html',
  styleUrls: ['./authentication.css'],
  imports: [RouterOutlet],
})
export class Authentication {

}
