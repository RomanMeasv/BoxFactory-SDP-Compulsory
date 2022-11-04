import {Component, OnInit} from '@angular/core';
import {BoxService} from "./box.service";
import {Box} from "./box.entity";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  boxWidth: number = 0;
  boxHeight: number = 0;
  boxes: Box[] = [];

  editingId?: number;
  editingWidth: number = 0;
  editingHeight: number = 0;

  constructor(private boxService: BoxService) {
  }

  ngOnInit(): void {
    this.readAllBoxes();
  }

  private readAllBoxes() {
    this.boxService.readAll()
      .subscribe(boxes => this.boxes = boxes);
  }

  createBox() {
    let dto = {
      width: this.boxWidth,
      height: this.boxHeight
    }
    this.boxService.createBox(dto)
      .subscribe(box => this.boxes.push(box));
  }

  deleteBox(id: number) {
    this.boxes = this.boxes.filter(b => b.id !== id)
    this.boxService.deleteBox(id).subscribe();
  }

  updateBox(id: number) {
    let boxToEdit = this.boxes.find(b => b.id === id);
    if(boxToEdit){
      boxToEdit.height = this.editingHeight;
      boxToEdit.width = this.editingWidth;
      this.boxService.updateBox(boxToEdit).subscribe();
    }
  }
}
