using System;
using System.Media;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Globalization;

namespace ListListList{
    public static class Vars{
        public static List<Task> todoList = new List<Task>();
        public static List<Done> doneList = new List<Done>();
        public static List<Task> un_todoList = new List<Task>();
        public static List<Done> un_doneList = new List<Done>();
        public static Stack<List<Task>> undo_todo = new Stack<List<Task>>();
        public static Stack<List<Task>> redo_todo = new Stack<List<Task>>(); 
        public static Stack<List<Done>> undo_done = new Stack<List<Done>>();
        public static Stack<List<Done>> redo_done = new Stack<List<Done>>();
        public static TextBox box_writeTask = new TextBox();
        public static TextBox box_inputDate = new TextBox();
        public static TextBox box_inputTime = new TextBox();
        public static TextBox box_watch = new TextBox();
        public static ListBox listBox_todo = new ListBox();
        public static ListBox listBox_done = new ListBox();
        public static DateTimePicker dp_date =new DateTimePicker();
        public static DateTimePicker dp_time =new DateTimePicker();
        public static Timer timer;
        public static bool alarm_on_flag = false;
        public static bool every_flag = false;
        public static bool alarmed_flag = true;
        public static SoundPlayer alarm_sound =new SoundPlayer("./sounds/8bitアラーム.wav");
        public static Color backColor_form =Color.FromArgb(56,60,159);
        public static Color backColor_tabF = Color.FromArgb(28,30,44);
        public static Color backColor_tabR =Color.FromArgb(6,10,109);
        public static Color backColor_button =Color.FromArgb(63,67,95);
        public static Color backColor_onoff =Color.FromArgb(248,79,150);
        public static Color backColor_every =Color.FromArgb(55,169,248);
        public static Color backColor_inputBox =Color.White;
        public static Color backColor_listBox =Color.FromArgb(45,50,60);
        public static Color backColor_buttonMouseEnter =Color.FromArgb(56,60,159);
        public static Color backColor_onoffMouseEnter =Color.FromArgb(255,86,157);
        public static Color backColor_everyMouseEnter =Color.FromArgb(62,176,255);
        public static Font font_title = new Font("Arial",12,FontStyle.Bold);
        public static Font font_cons = new Font("Consolas",11,FontStyle.Regular);
        public static Font font_arial = new Font("Arial",11,FontStyle.Regular);
        public static Color textColor1 =Color.White;
        public static Color textColor2 =Color.Black;
        public static Color textColor_list =Color.FromArgb(118,255,227);
        public static Color textColor_input =Color.Black;
        public static Color textColor_button =Color.FromArgb(118,255,227);
    }

    public class MainForm:Form{
        public MainForm(){
            CustomTabControl customTabControl = new CustomTabControl();

            this.Size =new Size(595,540);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0.95;

            Point mousePoint = new Point();
            
            int adjustLocation = -25;

            TabPage tabPage1 = new TabPage("ToDo");
            TabPage tabPage2 = new TabPage("Done");
            
            Label icon_tab1 = new Label();
            Label icon_tab2 = new Label();
            Label title = new Label();
            Label icon_title = new Label();
            Label label_writeTask = new Label();
            Label label_date = new Label();
            Label label_alarm = new Label();
            Label label_timeNow = new Label();
            Label label_todoList = new Label();
            Label label_done = new Label();

            Button button_undo = new Button();
            Button button_redo = new Button();
            Button button_save = new Button();
            Button button_min = new Button();
            Button button_close = new Button();
            RoundedButton button_add = new RoundedButton();
            RoundedButton button_update = new RoundedButton();
            RoundedButton button_delete_todo = new RoundedButton();
            RoundedButton button_check = new RoundedButton();
            RoundedButton toggle_alarm = new RoundedButton();
            RoundedButton toggle_every = new RoundedButton();
            RoundedButton button_PutBack = new RoundedButton();
            RoundedButton button_delete_done = new RoundedButton();

            customTabControl.Size = new Size(595,510);
            customTabControl.Location = new Point(0,30);
            customTabControl.ItemSize = new Size(120,25);
            customTabControl.Multiline = true;

            tabPage1.BackColor = Vars.backColor_tabF;
            tabPage2.BackColor = Vars.backColor_tabF;

            Vars.dp_date.Size = new Size(180, 25);
            Vars.dp_date.Format = DateTimePickerFormat.Custom;
            Vars.dp_date.CustomFormat = "yyyy/MM/dd (ddd)";

            Vars.dp_time.Size = new Size(70,25);
            Vars.dp_time.Format = DateTimePickerFormat.Custom;
            Vars.dp_time.CustomFormat = "HH:mm";
            Vars.dp_time.ShowUpDown = true;

            icon_tab1.Image = Image.FromFile("./images/list.png");
            icon_tab1.Location = new Point(0,0);
            icon_tab1.Size = new Size(30,30);

            icon_tab2.Image = Image.FromFile("./images/check.png");
            icon_tab2.Location = new Point(0,0);
            icon_tab2.Size = new Size(30,30);

            title.Text = "      List! List! List!";
            title.Font = Vars.font_title;
            title.TextAlign = ContentAlignment.MiddleLeft;
            title.ForeColor = Vars.textColor1;
            title.BackColor = Vars.backColor_form;
            title.Location = new Point(0,0);
            title.Size = new Size(600,30);

            icon_title.Image = Image.FromFile("./images/list.png");
            icon_title.Location = new Point(0,0);
            icon_title.Size = new Size(30,30);

            label_writeTask.Text = "Task";
            label_writeTask.Font = Vars.font_arial;
            label_writeTask.TextAlign = ContentAlignment.TopLeft;
            label_writeTask.ForeColor = Vars.textColor1;
            label_writeTask.Location = new Point(40,45+adjustLocation);
            label_writeTask.Size = new Size(40,15);

            label_date.Text = "Date";
            label_date.Font = Vars.font_arial;
            label_date.TextAlign = ContentAlignment.TopCenter;
            label_date.ForeColor = Vars.textColor1;
            label_date.Location = new Point(375,45+adjustLocation);
            label_date.Size = new Size(40,15);

            label_alarm.Text = "Alarm";
            label_alarm.Font = Vars.font_arial;
            label_alarm.TextAlign = ContentAlignment.TopLeft;
            label_alarm.ForeColor = Vars.textColor1;
            label_alarm.Location = new Point(375,100+adjustLocation);
            label_alarm.Size = new Size(50,15);

            label_timeNow.Text = "Now";
            label_timeNow.Font = Vars.font_arial;
            label_timeNow.TextAlign = ContentAlignment.TopCenter;
            label_timeNow.ForeColor = Vars.textColor1;
            label_timeNow.Location = new Point(420,165+adjustLocation);
            label_timeNow.Size = new Size(40,15);

            label_todoList.Text = "ToDo";
            label_todoList.Font = Vars.font_arial;
            label_todoList.TextAlign = ContentAlignment.TopLeft;
            label_todoList.ForeColor = Vars.textColor1;
            label_todoList.Location = new Point(40,200+adjustLocation);
            label_todoList.Size = new Size(45,15);

            label_done.Text = "Done";
            label_done.Font = Vars.font_arial;
            label_done.TextAlign = ContentAlignment.TopLeft;
            label_done.ForeColor = Vars.textColor1;
            label_done.Location = new Point(40,45 + adjustLocation);
            label_done.Size = new Size(45,15);

            button_undo.Image = Image.FromFile("./images/undo.png");
            button_undo.FlatStyle =FlatStyle.Flat;
            button_undo.FlatAppearance.BorderSize = 0;
            button_undo.BackColor = Vars.backColor_form;
            button_undo.Size = new Size(30,30);
            button_undo.Location = new Point(190,0);
            button_undo.Click += delegate(object sender,EventArgs e){
                if(Vars.undo_todo.Count > 0){
                    Vars.redo_todo.Push(Vars.todoList);
                    Vars.todoList = new List<Task>(Vars.undo_todo.Pop());
                    Vars.listBox_todo.DataSource = null;
                    Vars.listBox_todo.DataSource = Vars.todoList.ToList();
                    Vars.listBox_todo.DisplayMember = "list_Row";
                }
                if(Vars.undo_done.Count > 0){
                    Vars.redo_done.Push(Vars.doneList);
                    Vars.doneList = new List<Done>(Vars.undo_done.Pop());
                    Vars.listBox_done.DataSource = null;
                    Vars.listBox_done.DataSource = Vars.doneList.ToList();
                    Vars.listBox_done.DisplayMember = "list_Row";
                }
            };

            button_redo.Image = Image.FromFile("./images/redo.png");
            button_redo.FlatStyle =FlatStyle.Flat;
            button_redo.FlatAppearance.BorderSize = 0;
            button_redo.BackColor = Vars.backColor_form;
            button_redo.Size = new Size(30,30);
            button_redo.Location = new Point(220,0);
            button_redo.Click += delegate(object sender,EventArgs e){
                if(Vars.redo_todo.Count > 0){
                    Vars.undo_todo.Push(Vars.todoList);
                    if(Vars.undo_todo.Count == 51){
                        List<List<Task>> list = new List<List<Task>>(Vars.undo_todo);
                        list.RemoveAt(0);
                        Vars.undo_todo = new Stack<List<Task>>(list);
                    }
                    Vars.todoList = new List<Task>(Vars.redo_todo.Pop());
                    Vars.listBox_todo.DataSource = null;
                    Vars.listBox_todo.DataSource = Vars.todoList.ToList();
                    Vars.listBox_todo.DisplayMember = "list_Row";
                }
                if(Vars.redo_done.Count > 0){
                    Vars.undo_done.Push(Vars.doneList);
                    if(Vars.undo_done.Count == 51){
                        List<List<Done>> list = new List<List<Done>>(Vars.undo_done);
                        list.RemoveAt(0);
                        Vars.undo_done = new Stack<List<Done>>(list);
                    }
                    Vars.doneList = new List<Done>(Vars.redo_done.Pop());
                    Vars.listBox_done.DataSource = null;
                    Vars.listBox_done.DataSource = Vars.doneList.ToList();
                    Vars.listBox_done.DisplayMember = "list_Row";
                }
            };

            button_save.Image = Image.FromFile("./images/save.png");
            button_save.FlatStyle =FlatStyle.Flat;
            button_save.FlatAppearance.BorderSize = 0;
            button_save.BackColor = Vars.backColor_form;
            button_save.Size = new Size(30,30);
            button_save.Location = new Point(505,0);
            button_save.Click += delegate(object sender,EventArgs e){
                string fileName = "./data/todo_list.xml";
                XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
                using (FileStream stream = File.Create(fileName)){
                    serializer.Serialize(stream, Vars.todoList);
                }
                fileName = "./data/done_list.xml";
                serializer = new XmlSerializer(typeof(List<Done>));
                using (FileStream stream = File.Create(fileName)){
                    serializer.Serialize(stream, Vars.doneList);
                }
                Vars.box_writeTask.Text = "info : Data saved successfully.";
            };

            button_min.Image = Image.FromFile("./images/min.png");
            button_min.FlatStyle =FlatStyle.Flat;
            button_min.FlatAppearance.BorderSize = 0;
            button_min.BackColor = Vars.backColor_form;
            button_min.Size = new Size(30,30);
            button_min.Location = new Point(535,0);
            button_min.Click += delegate(object sender,EventArgs e){
                this.WindowState = FormWindowState.Minimized;
            };

            button_close.Image = Image.FromFile("./images/close.png");
            button_close.FlatStyle =FlatStyle.Flat;
            button_close.FlatAppearance.BorderSize = 0;
            button_close.BackColor = Vars.backColor_form;
            button_close.Size = new Size(30,30);
            button_close.Location = new Point(565,0);
            button_close.Click += delegate(object sender,EventArgs e){
                Vars.timer.Enabled = false;
                Close();
            };

            button_add.Text = "Add";
            button_add.Font = Vars.font_arial;
            button_add.FlatStyle = FlatStyle.Flat;
            button_add.FlatAppearance.BorderSize = 0;
            button_add.TextAlign = ContentAlignment.MiddleCenter;
            button_add.ForeColor = Vars.textColor_button;
            button_add.BackColor = Vars.backColor_button;
            button_add.Location = new Point(40,160+adjustLocation);
            button_add.Size = new Size(40,24);
            button_add.Click += delegate(object sender,EventArgs e){
                PushUndo();
                Task task = new Task();
                if(Vars.every_flag){
                    task.set_date = "-Everyday-";
                }else{
                    task.set_date = Vars.dp_date.Value.ToString("yyyy-MM-dd");
                }
                if(Vars.alarm_on_flag){
                    task.set_time = Vars.dp_time.Text + ":00";
                }else{
                    task.set_time = "-Off-";
                }
                task.sentence = Vars.box_writeTask.Text;
                Vars.todoList.Add(task);
                UpdateList();
                Clear_TaskForm();
                Vars.listBox_todo.SetSelected(Vars.todoList.Count-1,true);
            };

            button_update.Text = "Update";
            button_update.Font = Vars.font_arial;
            button_update.FlatStyle =FlatStyle.Flat;
            button_update.FlatAppearance.BorderSize = 0;
            button_update.TextAlign = ContentAlignment.MiddleCenter;
            button_update.ForeColor = Vars.textColor_button;
            button_update.BackColor = Vars.backColor_button;
            button_update.Location = new Point(button_add.Width + button_add.Left + 10,button_add.Top);
            button_update.Size = new Size(60,24);
            button_update.Click += delegate(object sender,EventArgs e){
                PushUndo();
                if(Vars.listBox_todo.SelectedItem != null){
                    int index = Vars.listBox_todo.SelectedIndex;
                    Task task = new Task();
                    if(Vars.every_flag){
                        task.set_date = "-Everyday-";
                    }else{
                        task.set_date = Vars.dp_date.Value.ToString("yyyy-MM-dd");
                    }
                    if(Vars.alarm_on_flag){
                        task.set_time = Vars.dp_time.Text + ":00";
                    }else{
                        task.set_time = "-Off-";
                    }
                    task.sentence = Vars.box_writeTask.Text;
                    Vars.todoList[index] = task;
                    UpdateList();
                    Clear_TaskForm();
                    Vars.listBox_todo.SetSelected(index,true);
                }
            };

            button_delete_todo.Text = "Delete";
            button_delete_todo.Font = Vars.font_arial;
            button_delete_todo.FlatStyle =FlatStyle.Flat;
            button_delete_todo.FlatAppearance.BorderSize = 0;
            button_delete_todo.TextAlign = ContentAlignment.MiddleCenter;
            button_delete_todo.ForeColor = Vars.textColor_button;
            button_delete_todo.BackColor = Vars.backColor_button;
            button_delete_todo.Location = new Point(button_update.Width + button_update.Left + 10, button_add.Top);
            button_delete_todo.Size = new Size(56,24);
            button_delete_todo.Click += delegate(object sender,EventArgs e){
                PushUndo();
                if(Vars.listBox_todo.SelectedItem != null){
                    int index = Vars.listBox_todo.SelectedIndex;
                    Vars.todoList.RemoveAt(index);
                    UpdateList();
                    Clear_TaskForm();
                    if(index-1 > 0){
                        Vars.listBox_todo.SetSelected(index-1,true);
                    }
                }
            };

            button_check.Text = "✔";
            button_check.Font = Vars.font_arial;
            button_check.FlatStyle =FlatStyle.Flat;
            button_check.FlatAppearance.BorderSize = 0;
            button_check.TextAlign = ContentAlignment.MiddleCenter;
            button_check.ForeColor = Vars.textColor_button;
            button_check.BackColor = Vars.backColor_button;
            button_check.Location = new Point(button_delete_todo.Width + button_delete_todo.Left + 10, button_add.Top);
            button_check.Size = new Size(30,24);
            button_check.Click += delegate(object sender,EventArgs e){
                PushUndo();
                if(Vars.listBox_todo.SelectedItem != null){
                    int index = Vars.listBox_todo.SelectedIndex;
                    Done done = new Done();
                    done.set_date = Vars.todoList[index].set_date;
                    done.set_time = Vars.todoList[index].set_time;
                    done.sentence = Vars.todoList[index].sentence;
                    Vars.doneList.Add(done);
                    Vars.todoList.RemoveAt(index);
                    UpdateList();
                    Clear_TaskForm();
                    if(index-1 > 0){
                        Vars.listBox_todo.SetSelected(index-1, true);
                    }
                }
            };

            toggle_alarm.Text = "Off";
            toggle_alarm.Font = Vars.font_arial;
            toggle_alarm.FlatStyle =FlatStyle.Flat;
            toggle_alarm.FlatAppearance.BorderSize = 0;
            toggle_alarm.TextAlign = ContentAlignment.MiddleCenter;
            toggle_alarm.ForeColor = Vars.textColor_button;
            toggle_alarm.BackColor = Vars.backColor_button;
            toggle_alarm.Location = new Point(475, 120 + adjustLocation);
            toggle_alarm.Size = new Size(40,24);
            toggle_alarm.Click += delegate(object sender,EventArgs e){
                if(!Vars.alarm_on_flag){
                    toggle_alarm.BackColor = Vars.backColor_onoff;
                    toggle_alarm.ForeColor = Vars.textColor1;
                    toggle_alarm.Text = "On!";
                    Vars.alarm_on_flag = true;
                }else{
                    toggle_alarm.BackColor = Vars.backColor_button;
                    toggle_alarm.ForeColor = Vars.textColor_button;
                    Vars.alarm_on_flag = false;
                    toggle_alarm.Text = "Off";
                }
            };

            toggle_every.Text = "1";
            toggle_every.Font = Vars.font_arial;
            toggle_every.FlatStyle =FlatStyle.Flat;
            toggle_every.FlatAppearance.BorderSize = 0;
            toggle_every.TextAlign = ContentAlignment.MiddleCenter;
            toggle_every.ForeColor = Vars.textColor_button;
            toggle_every.BackColor = Vars.backColor_button;
            toggle_every.Location = new Point(525, 120 + adjustLocation);
            toggle_every.Size = new Size(25,24);
            toggle_every.Click += delegate(object sender,EventArgs e){
                if(!Vars.every_flag){
                    toggle_every.BackColor = Vars.backColor_every;
                    toggle_every.ForeColor = Vars.textColor1;
                    toggle_every.Text = "E";
                    Vars.every_flag = true;
                }else{
                    toggle_every.BackColor = Vars.backColor_button;
                    toggle_every.ForeColor = Vars.textColor_button;
                    Vars.every_flag = false;
                    toggle_every.Text = "1";
                }
            };

            button_PutBack.Text = "PutBack";
            button_PutBack.Font = Vars.font_arial;
            button_PutBack.FlatStyle =FlatStyle.Flat;
            button_PutBack.FlatAppearance.BorderSize = 0;
            button_PutBack.TextAlign = ContentAlignment.MiddleCenter;
            button_PutBack.ForeColor = Vars.textColor_button;
            button_PutBack.BackColor = Vars.backColor_button;
            button_PutBack.Location = new Point(150, 41 + adjustLocation);
            button_PutBack.Size = new Size(70,24);
            button_PutBack.Click += delegate(object sender,EventArgs e){
                PushUndo();
                if(Vars.listBox_done.SelectedItem != null){
                    int index = Vars.listBox_done.SelectedIndex;
                    Task task = new Task();
                    task.set_date = Vars.doneList[index].set_date;
                    task.set_time = Vars.doneList[index].set_time;
                    task.sentence = Vars.doneList[index].sentence;
                    Vars.todoList.Add(task);
                    Vars.doneList.RemoveAt(index);
                    UpdateList();
                    Clear_TaskForm();
                    if(index-1 > 0){
                        Vars.listBox_done.SetSelected(Vars.doneList.Count-1, true);
                    }
                }
            };

            button_delete_done.Text = "Delete";
            button_delete_done.Font = Vars.font_arial;
            button_delete_done.FlatStyle =FlatStyle.Flat;
            button_delete_done.FlatAppearance.BorderSize = 0;
            button_delete_done.TextAlign = ContentAlignment.MiddleCenter;
            button_delete_done.ForeColor = Vars.textColor_button;
            button_delete_done.BackColor = Vars.backColor_button;
            button_delete_done.Location = new Point(button_PutBack.Width + button_PutBack.Left + 10, button_PutBack.Top);
            button_delete_done.Size = new Size(56,24);
            button_delete_done.Click += delegate(object sender,EventArgs e){
                PushUndo();
                if(Vars.listBox_done.SelectedItem != null){
                    int index = Vars.listBox_done.SelectedIndex;
                    Vars.doneList.RemoveAt(index);
                    UpdateList();
                    Clear_TaskForm();
                    if(index-1 > 0){
                        Vars.listBox_done.SetSelected(index-1, true);
                    }
                }
            };

            Vars.box_writeTask.Location = new Point(40,65 + adjustLocation);
            Vars.box_writeTask.Multiline = true;
            Vars.box_writeTask.Size = new Size(320,80);
            Vars.box_writeTask.ScrollBars = ScrollBars.Vertical;
            Vars.box_writeTask.Font = Vars.font_cons;
            Vars.box_writeTask.ForeColor = Vars.textColor_input;
            Vars.box_writeTask.BackColor = Vars.backColor_inputBox;
            Vars.box_writeTask.BorderStyle = BorderStyle.Fixed3D;

            Vars.box_inputDate.Location = new Point(375,65 + adjustLocation);
            Vars.box_inputDate.Size = new Size(176,25);
            Vars.box_inputDate.Font = Vars.font_cons;
            Vars.box_inputDate.ForeColor = Vars.textColor_input;
            Vars.box_inputDate.BackColor = Vars.backColor_inputBox;
            Vars.box_inputDate.BorderStyle = BorderStyle.FixedSingle;
            Vars.box_inputDate.Controls.Add(Vars.dp_date);
            
            Vars.box_watch.Location = new Point(460, button_add.Top);
            Vars.box_watch.Multiline = true;
            Vars.box_watch.Size = new Size(90,25);
            Vars.box_watch.Font = Vars.font_title;
            Vars.box_watch.TextAlign = HorizontalAlignment.Center;
            Vars.box_watch.ForeColor = Vars.textColor_list;
            Vars.box_watch.BackColor = Vars.backColor_button;
            Vars.box_watch.BorderStyle = BorderStyle.FixedSingle;

            Vars.box_inputTime.Location = new Point(375,120+adjustLocation);
            Vars.box_inputTime.Size = new Size(70,25);
            Vars.box_inputTime.Font = Vars.font_cons;
            Vars.box_inputTime.ForeColor = Vars.textColor_input;
            Vars.box_inputTime.BackColor = Vars.backColor_inputBox;
            Vars.box_inputTime.BorderStyle = BorderStyle.FixedSingle;
            Vars.box_inputTime.Controls.Add(Vars.dp_time);

            Vars.listBox_todo.Location = new Point(40,220+adjustLocation);
            Vars.listBox_todo.Size = new Size(510,260);
            Vars.listBox_todo.Font = Vars.font_cons;
            Vars.listBox_todo.ForeColor = Vars.textColor_list;
            Vars.listBox_todo.BackColor = Vars.backColor_listBox;
            Vars.listBox_todo.BorderStyle = BorderStyle.FixedSingle;
            Vars.listBox_todo.SelectedIndexChanged += delegate(object sender, EventArgs e){
                if(Vars.listBox_todo.SelectedItem != null){
                    int index = Vars.listBox_todo.SelectedIndex;
                    Task task = Vars.todoList[index];
                    Vars.box_writeTask.Text = task.sentence;
                    if(task.set_date != "-Everyday-" && task.set_date != "-I_did_it-"){
                        Vars.dp_date.Text = task.set_date;
                    }else{
                        Vars.dp_date.Text = Vars.dp_date.Value.ToString("yyyy-MM-dd");
                    }
                    if(task.set_time != "-Off-" && task.set_time != "-o_k-"){
                        Vars.dp_time.Text = task.set_time;
                    }else{
                        Vars.dp_time.Text = DateTime.Now.ToString("HH:mm:ss");
                    }
                }
            };

            Vars.listBox_done.Location = new Point(40,76 + adjustLocation);
            Vars.listBox_done.Size = new Size(510,400);
            Vars.listBox_done.Font = Vars.font_cons;
            Vars.listBox_done.ForeColor = Vars.textColor_list;
            Vars.listBox_done.BackColor = Vars.backColor_listBox;
            Vars.listBox_done.BorderStyle = BorderStyle.FixedSingle;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(title);
            title.Controls.Add(icon_title);
            title.Controls.Add(button_undo);
            title.Controls.Add(button_redo);
            title.Controls.Add(button_save);
            title.Controls.Add(button_min);
            title.Controls.Add(button_close);
            tabPage1.Controls.Add(icon_tab1);
            tabPage1.Controls.Add(label_writeTask);
            tabPage1.Controls.Add(label_date);
            tabPage1.Controls.Add(label_todoList);
            tabPage1.Controls.Add(label_alarm);
            tabPage1.Controls.Add(label_timeNow);
            tabPage1.Controls.Add(button_add);
            tabPage1.Controls.Add(button_update);
            tabPage1.Controls.Add(button_delete_todo);
            tabPage1.Controls.Add(button_check);
            tabPage1.Controls.Add(Vars.box_writeTask);
            tabPage1.Controls.Add(Vars.box_inputDate);
            tabPage1.Controls.Add(Vars.box_watch);
            tabPage1.Controls.Add(Vars.box_inputTime);
            tabPage1.Controls.Add(toggle_alarm);
            tabPage1.Controls.Add(toggle_every);
            tabPage1.Controls.Add(Vars.listBox_todo);
            tabPage2.Controls.Add(icon_tab2);
            tabPage2.Controls.Add(label_done);
            tabPage2.Controls.Add(button_PutBack);
            tabPage2.Controls.Add(button_delete_done);
            tabPage2.Controls.Add(Vars.listBox_done);
            customTabControl.TabPages.Add(tabPage1);
            customTabControl.TabPages.Add(tabPage2);
            this.Controls.Add(customTabControl);

            this.MouseDown += delegate(object sender, MouseEventArgs e){
                if((e.Button & MouseButtons.Left) == MouseButtons.Left){
                    mousePoint = new Point(e.X,e.Y);
                }
            };

            this.MouseMove += delegate(object sender, MouseEventArgs e){
                if((e.Button & MouseButtons.Left) == MouseButtons.Left){
                    this.Left += e.X - mousePoint.X;
                    this.Top += e.Y - mousePoint.Y;
                }
            };

            title.MouseDown += delegate(object sender, MouseEventArgs e){
                if((e.Button & MouseButtons.Left) == MouseButtons.Left){
                    mousePoint = new Point(e.X,e.Y);
                }
            };

            title.MouseMove += delegate(object sender, MouseEventArgs e){
                if((e.Button & MouseButtons.Left) == MouseButtons.Left){
                    this.Left += e.X - mousePoint.X;
                    this.Top += e.Y - mousePoint.Y;
                }
            };            

            button_close.MouseEnter += delegate(object sender, EventArgs e){
                button_close.BackColor = Color.Red;
            };
            button_close.MouseLeave += delegate(object sender, EventArgs e){
                button_close.BackColor = Vars.backColor_form;
            };

            button_add.MouseEnter += delegate(object sender, EventArgs e){
                button_add.BackColor = Vars.backColor_buttonMouseEnter;
            };
            button_add.MouseLeave += delegate(object sender, EventArgs e){
                button_add.BackColor = Vars.backColor_button;
            };

            button_update.MouseEnter += delegate(object sender, EventArgs e){
                button_update.BackColor = Vars.backColor_buttonMouseEnter;
            };
            button_update.MouseLeave += delegate(object sender, EventArgs e){
                button_update.BackColor = Vars.backColor_button;
            };

            button_delete_todo.MouseEnter += delegate(object sender, EventArgs e){
                button_delete_todo.BackColor = Vars.backColor_buttonMouseEnter;
            };
            button_delete_todo.MouseLeave += delegate(object sender, EventArgs e){
                button_delete_todo.BackColor = Vars.backColor_button;
            };

            button_check.MouseEnter += delegate(object sender, EventArgs e){
                button_check.BackColor = Vars.backColor_buttonMouseEnter;
            };
            button_check.MouseLeave += delegate(object sender, EventArgs e){
                button_check.BackColor = Vars.backColor_button;
            };

            toggle_alarm.MouseEnter += delegate(object sender, EventArgs e){
                if(Vars.alarm_on_flag){
                    toggle_alarm.BackColor = Vars.backColor_onoffMouseEnter;
                }else{
                    toggle_alarm.BackColor = Vars.backColor_buttonMouseEnter;
                }
            };
            toggle_alarm.MouseLeave += delegate(object sender, EventArgs e){
                if(Vars.alarm_on_flag){
                    toggle_alarm.BackColor = Vars.backColor_onoff;
                }else{
                    toggle_alarm.BackColor = Vars.backColor_button;
                }
            };

            toggle_every.MouseEnter += delegate(object sender, EventArgs e){
                if(Vars.every_flag){
                    toggle_every.BackColor = Vars.backColor_everyMouseEnter;
                }else{
                    toggle_every.BackColor = Vars.backColor_buttonMouseEnter;
                }
            };
            toggle_every.MouseLeave += delegate(object sender, EventArgs e){
                if(Vars.every_flag){
                    toggle_every.BackColor = Vars.backColor_every;
                }else{
                    toggle_every.BackColor = Vars.backColor_button;
                }
            };

            button_PutBack.MouseEnter += delegate(object sender, EventArgs e){
                button_PutBack.BackColor = Vars.backColor_buttonMouseEnter;
            };
            button_PutBack.MouseLeave += delegate(object sender, EventArgs e){
                button_PutBack.BackColor = Vars.backColor_button;
            };

            button_delete_done.MouseEnter += delegate(object sender, EventArgs e){
                button_delete_done.BackColor = Vars.backColor_buttonMouseEnter;
            };
            button_delete_done.MouseLeave += delegate(object sender, EventArgs e){
                button_delete_done.BackColor = Vars.backColor_button;
            };

            LoadList();
            UpdateList();
            Run_Timer();
        }

        public static void UpdateList(){
            Vars.listBox_todo.DataSource = null;
            Vars.listBox_todo.DataSource = Vars.todoList.ToList();
            Vars.listBox_todo.DisplayMember = "list_row";
            Vars.listBox_done.DataSource = null;
            Vars.listBox_done.DataSource = Vars.doneList.ToList();
            Vars.listBox_done.DisplayMember = "list_row";
        }

        public static void PushUndo(){
            Vars.undo_todo.Push(new List<Task>(Vars.todoList));
            Vars.undo_done.Push(new List<Done>(Vars.doneList));
            if(Vars.undo_todo.Count == 51){
                List<List<Task>> list = new List<List<Task>>(Vars.undo_todo);
                list.RemoveAt(0);
                Vars.undo_todo = new Stack<List<Task>>(list);
            }
            if(Vars.undo_done.Count == 51){
                List<List<Done>> list = new List<List<Done>>(Vars.undo_done);
                list.RemoveAt(0);
                Vars.undo_done = new Stack<List<Done>>(list);
            }
            Vars.redo_todo.Clear();
            Vars.redo_done.Clear();
        }

        public static void Clear_TaskForm(){
            Vars.dp_date.Text = DateTime.Now.ToShortDateString();
            Vars.dp_time.Text = DateTime.Now.ToString("HH:mm:ss");
            Vars.box_writeTask.Text = string.Empty;
        }

        public static void LoadList(){
            string fileName_todo = Path.Combine(Environment.CurrentDirectory,"./data/todo_list.xml");
            if(File.Exists(fileName_todo)){
                XmlSerializer serializer_todo = new XmlSerializer(typeof(List<Task>));
                using(FileStream stream = File.OpenRead(fileName_todo)){
                    Vars.todoList = (List<Task>)serializer_todo.Deserialize(stream);
                }
            }
            string fileName_done = Path.Combine(Environment.CurrentDirectory,"./data/done_list.xml");
            if(File.Exists(fileName_done)){
                XmlSerializer serializer_done = new XmlSerializer(typeof(List<Done>));
                using(FileStream stream = File.OpenRead(fileName_done)){
                    Vars.doneList = (List<Done>)serializer_done.Deserialize(stream);
                }
            }
        }

        private void Run_Timer(){
            Vars.timer = new Timer();
            Vars.timer.Tick += Tick_Timer;
            Vars.timer.Interval = 1000;
            Vars.timer.Enabled = true;
            Vars.box_watch.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Tick_Timer(object sender,EventArgs e){
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string c_time = DateTime.Now.ToString("HH:mm:ss");
            Vars.box_watch.Text = c_time;
            for(int i=0;i<Vars.todoList.Count;i++){
                string date_al =Vars.todoList[i].set_date;
                string time_al =Vars.todoList[i].set_time;
                if("-Everyday-" + c_time == date_al + time_al || today + c_time == date_al + time_al){
                    AlarmForm alarm_form = new AlarmForm(Vars.todoList[i].time_row);
                    Vars.alarm_sound.Play();
                    alarm_form.Show();
                    alarm_form.Activate();
                    Run_Timer();
                }
            }
        }

        [STAThread]
        public static void Main(){
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }

    }
    public class Task{
        public string set_date {get; set;}
        public string set_time {get; set;}
        public string sentence {get; set;}
        public string list_row {
            get{
                if(set_time == "-Off-"){
                    return set_date + "[" + set_time.Substring(0,5) + "]📄 > " + sentence;
                }else if(set_time == "-o_k-"){
                    return set_date + "[" + set_time.Substring(0,5) + "]✔ > " + sentence;
                }else{
                    return set_date + "[" + set_time.Substring(0,5) + "]🔔 > " + sentence;
                }
            }
        }
        public string time_row{
            get{return set_time.Substring(0,5) + " - " + sentence;}    
        }
    }

    public class Done{
        public string set_date {get; set;}
        public string set_time {get; set;}
        public string sentence {get; set;}
        public string list_row {
            get{return "-I_did_it-" + "[-o_k-]✔ > " + sentence;}
        }
    }

    public class AlarmForm:Form{
        private TextBox box_task = new TextBox();
        private Point mousePoint = new Point();

        public AlarmForm(string sentence){
            Label title = new Label();
            Label label_task = new Label();

            Button button_close = new Button();

            int adjustLocation = 20;

            this.Size = new Size(320,200);
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;

            title.Text = "-- Alarm --";
            title.Font = Vars.font_title;
            title.ForeColor = Vars.textColor2;
            title.Location = new Point(20,9);
            title.Size = new Size(130,25);

            label_task.Text = "アラーム";
            label_task.Font = Vars.font_cons;
            label_task.ForeColor = Vars.textColor2;
            label_task.Location = new Point(40,40+adjustLocation);
            label_task.Size = new Size(100,15);

            button_close.Image = Image.FromFile("./images/close.png");
            button_close.FlatStyle = FlatStyle.Flat;
            button_close.FlatAppearance.BorderSize = 0;
            button_close.BackColor = Vars.backColor_button;
            button_close.Size = new Size(20,20);
            button_close.Location = new Point(290,5);
            button_close.Click += delegate(object sender,EventArgs e){
                Vars.timer.Enabled = false;
                Close();
            };

            box_task.Location = new Point(40,65+adjustLocation);
            box_task.Multiline = true;
            box_task.Size = new Size(250,50);
            box_task.ScrollBars = ScrollBars.Vertical;
            box_task.Font = Vars.font_arial;
            box_task.BorderStyle = BorderStyle.Fixed3D;
            box_task.Text = sentence;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(title);
            this.Controls.Add(label_task);
            this.Controls.Add(button_close);
            this.Controls.Add(box_task);

            this.MouseDown += delegate(object sender, MouseEventArgs e){
                if((e.Button & MouseButtons.Left) == MouseButtons.Left){
                    mousePoint = new Point(e.X,e.Y);
                }
            };

            this.MouseMove += delegate(object sender, MouseEventArgs e){
                if((e.Button & MouseButtons.Left) == MouseButtons.Left){
                    this.Left += e.X - mousePoint.X;
                    this.Top += e.Y - mousePoint.Y;
                }
            };

            title.MouseDown += delegate(object sender, MouseEventArgs e){
                if((e.Button & MouseButtons.Left) == MouseButtons.Left){
                    mousePoint = new Point(e.X,e.Y);
                }
            };

            title.MouseMove += delegate(object sender, MouseEventArgs e){
                if((e.Button & MouseButtons.Left) == MouseButtons.Left){
                    this.Left += e.X - mousePoint.X;
                    this.Top += e.Y - mousePoint.Y;
                }
            };                
        }
    }

    public class CustomTabControl : TabControl{
        public CustomTabControl() : base(){
            this.SetStyle(ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.SizeMode = TabSizeMode.Fixed;
            this.Appearance = TabAppearance.Normal;
        }

        protected override void OnPaint(PaintEventArgs e){
            base.OnPaint(e);
            Brush brush_backF = new SolidBrush(Vars.backColor_tabF);
            Brush brush_backR = new SolidBrush(Vars.backColor_tabR);
            Brush brush_textF = new SolidBrush(Vars.textColor_list);
            Brush brush_textR = new SolidBrush(Color.Gray);
            e.Graphics.Clear(Vars.backColor_form);

            for(int i=0; i<this.TabPages.Count; i++){
                TabPage page = this.TabPages[i];
                Rectangle pageRect = new Rectangle(
                    page.Bounds.X,
                    page.Bounds.Y,
                    page.Bounds.Width,
                    page.Bounds.Height
                );
                TabRenderer.DrawTabPage(e.Graphics, pageRect);
                string tabText = page.Text;
                Rectangle tabRect =GetTabRect(i);
                tabRect.X += 2;
                tabRect.Width -= 3;
                tabRect.Height += 2;
                GraphicsPath path = GetRoundedRectanglePath(tabRect);
                Font font = Vars.font_title;
                StringFormat format = new StringFormat{
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                if(this.SelectedIndex == i){
                    e.Graphics.FillPath(brush_backF, path);
                    e.Graphics.DrawString(tabText, font, brush_textF, tabRect, format);
                }else{
                    e.Graphics.FillPath(brush_backR, path);
                    e.Graphics.DrawString(tabText, font, brush_textR, tabRect, format);
                }
            }
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, 10, 10, 180, 90);
            path.AddArc(rect.Right - 10, rect.Y, 10, 10, 270, 90);
            path.AddArc(rect.Right - 1, rect.Bottom - 1, 1, 1, 0, 90);
            path.AddArc(rect.X, rect.Bottom - 1, 1, 1, 90, 90);
            path.CloseFigure();

            return path;
        }
    }

    public class RoundedButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, 10, 10, 180, 90);
                path.AddArc(Width - 10, 0, 10, 10, 270, 90);
                path.AddArc(Width - 10, Height - 10, 10, 10, 0, 90);
                path.AddArc(0, Height - 10, 10, 10, 90, 90);
                path.CloseFigure();

                this.Region = new Region(path);
                g.FillPath(new SolidBrush(this.BackColor), path);
            }
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), 5, 3);
        }
    }
}

