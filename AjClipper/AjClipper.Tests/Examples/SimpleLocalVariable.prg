
procedure SetBar
public bar
bar := "publicbar"
return

procedure SetLocalBar
local bar
public foo
bar := "localbar"
do SetBar()
foo := bar
return

do SetLocalBar()

