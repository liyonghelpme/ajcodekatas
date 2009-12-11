
procedure SetPrivateBar
bar := "privatebar"
return

procedure SetBar
private bar
public foo
do SetPrivateBar()
foo := bar
return

do SetBar()

