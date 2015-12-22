## CaptureScreen

Using OS X, we can capture screen by press "Command+Shift+3".

OS X will capture the current screen and save to a picture on the desktop.

However, Win7 can't do such simple thing.

So, I create a winform application for this use case.

## User Manual

* select a work directory (D:\ by default)

* click "启动", the tool will create **a temporatory folder** in the work directory.

* press "Ctrl+PrintScr", the tool will create a picture of the current screen to that directory.

* after many times "Ctrl+PrintScr", the tool will create several pictures in the **same directory**.

* click "停止", the tool will disable the hot key.

* when you click "启动" once again, the tool will create a new temporatory folder to hold the pictures.
