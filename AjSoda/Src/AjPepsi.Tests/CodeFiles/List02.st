
Object new [^self class basicNew initialize]
Object initialize []

List : Object(head tail)

List head [^head]
List tail [^tail]
List head: newHead [head := newHead]
List tail: newTail [tail := newTail]

list1 := [^List new]
list2 := [^List new]

[list1 head: 'Hello']
[list2 head: 'World']
[list1 tail: list2]

