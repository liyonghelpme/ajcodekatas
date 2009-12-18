/*
 * Source     : AXCDXSIX.CH for CA-Clipper 5.2
 * Description: Header file for the Advantage CDX/IDX RDD (DBFCDXAX).
 *              Include this file in your application if you want to
 *              use both the Advantage CDX/IDX RDD (DBFCDXAX) and the
 *              SuccessWare CDX/IDX RDD (SIXCDX) in the same application.
 *              If you include the SIXCDX.CH header file in your application,
 *              include it before this header file.  Link either DBFCDXAX.OBJ
 *              or SIXCDX.OBJ into your application to make either DBFCDXAX
 *              or SIXCDX, respectively, the default RDD.
 *
 * Copyright 1996 - Extended Systems, Inc.
 */

REQUEST DBFCDXAX
REQUEST SIXCDX


#command SET PASSWORD TO <(password)>                                      ;
      => AX_SetPass( <(password)> )

#command SET PASSWORD TO                                                   ;
      => AX_SetPass( "" )

#command BEGIN TRANSACTION                                                 ;
      => dbCommitAll()                                                     ;
       ; AX_Transaction( 1 )

#command COMMIT TRANSACTION                                                ;
      => dbCommitAll()                                                     ;
       ; AX_Transaction( 2 )

#command ROLLBACK TRANSACTION                                              ;
      => dbCommitAll()                                                     ;
       ; AX_Transaction( 3 )

#command SET RIGHTS CHECKING <x:ON,OFF>                                    ;
      => AX_RightsCheck( if( upper( <(x)> ) == "ON", .t., .f. ) )

#command SET EXPRESSION ENGINE <x:ON,OFF>                                  ;
      => AX_ExprEngine( if( upper( <(x)> ) == "ON", .t., .f. ) )

#command SET AXS LOCKING <x:ON,OFF>                                        ;
      => AX_AXSLocking( if( upper( <(x)> ) == "ON", .t., .f. )  )

#command SET TAGORDER TO <order>                                           ;
      => ordSetFocus( <order> )

#command SET TAGORDER TO                                                   ;
      => ordSetFocus( 0 )

#command SET ORDER TO TAG <(tag)>                                          ;
         [OF <(bag)>]                                                      ;
         [IN <(bag)>]                                                      ;
      => ordSetFocus( <(tag)> [, <(bag)>] )

#command SET TAG TO <(tag)>                                                ;
         [OF <(bag)>]                                                      ;
         [IN <(bag)>]                                                      ;
      => ordSetFocus( <(tag)> [, <(bag)>] )

#command SET TAG TO                                                        ;
      => ordSetFocus( 0 )


#command INDEX ON <key> TO <(file)>                                        ;
         [FOR       <for>]                                                 ;
         [<all:     ALL>]                                                  ;
         [WHILE     <while>]                                               ;
         [NEXT      <next>]                                                ;
         [RECORD    <rec>]                                                 ;
         [<rest:    REST>]                                                 ;
         [EVAL      <eval> [EVERY  <every>]]                               ;
         [OPTION    <eval> [STEP   <every>]]                               ;
         [<unique:  UNIQUE>]                                               ;
         [<ascend:  ASCENDING>]                                            ;
         [<descend: DESCENDING>]                                           ;
         [<cur:     USECURRENT>]                                           ;
         [<cur:     SUBINDEX>]                                             ;
         [<add:     ADDITIVE>]                                             ;
         [<non:     NONCOMPACT>]                                           ;
         [<custom:  CUSTOM>]                                               ;
         [<custom:  EMPTY>]                                                ;
      => ordCondSet( <"for">, <{for}>,                                     ;
                     if( <.all.>, .t., NIL ),                              ;
                     <{while}>,                                            ;
                     <{eval}>, <every>,                                    ;
                     RECNO(), <next>, <rec>,                               ;
                     if( <.rest.>, .t., NIL ),                             ;
                     if( (<.descend.> .AND. !<.ascend.>), .t., NIL ),      ;
                     .f., NIL, <.cur.>, <.custom.>, <.non.>, <.add.>, NIL, ;
                     <"while"> )                                           ;
       ; dbCreateIndex( <(file)>, <"key">, <{key}>,                        ;
                        if( <.unique.>, .t., NIL )  )

#command INDEX ON <key> TAG <(tag)>                                        ;
         [OF <(bag)>]                                                      ;
         [TO <(bag)>]                                                      ;
         [FOR        <for>]                                                ;
         [<all:      ALL>]                                                 ;
         [WHILE      <while>]                                              ;
         [NEXT       <next>]                                               ;
         [RECORD     <rec>]                                                ;
         [<rest:     REST>]                                                ;
         [EVAL       <eval> [EVERY  <every>]]                              ;
         [OPTION     <eval> [STEP   <every>]]                              ;
         [<unique:   UNIQUE>]                                              ;
         [<ascend:   ASCENDING>]                                           ;
         [<descend:  DESCENDING>]                                          ;
         [<cur:      USECURRENT>]                                          ;
         [<cur:      SUBINDEX>]                                            ;
         [<add:      ADDITIVE>]                                            ;
         [<custom:   CUSTOM>]                                              ;
         [<custom:   EMPTY>]                                               ;
      => ordCondSet( <"for">, <{for}>,                                     ;
                     if( <.all.>, .t., NIL ),                              ;
                     <{while}>,                                            ;
                     <{eval}>, <every>,                                    ;
                     RECNO(), <next>, <rec>,                               ;
                     if( <.rest.>, .t., NIL ),                             ;
                     if( (<.descend.> .AND. !<.ascend.>), .t., NIL ),      ;
                     .t., <(bag)>, <.cur.>, <.custom.>, .f., <.add.>, NIL, ;
                     <"while"> )                                           ;
       ; ordCreate( <(bag)>, <(tag)>, <"key">, <{key}>, [<.unique.>] )


#command SUBINDEX ON <key> TO <(file)>                                     ;
         [FOR       <for>]                                                 ;
         [<all:     ALL>]                                                  ;
         [WHILE     <while>]                                               ;
         [NEXT      <next>]                                                ;
         [RECORD    <rec>]                                                 ;
         [<rest:    REST>]                                                 ;
         [EVAL      <eval> [EVERY  <every>]]                               ;
         [OPTION    <eval> [STEP   <every>]]                               ;
         [<unique:  UNIQUE>]                                               ;
         [<ascend:  ASCENDING>]                                            ;
         [<descend: DESCENDING>]                                           ;
         [<non:     NONCOMPACT>]                                           ;
         [<add:     ADDITIVE>]                                             ;
         [<custom:  CUSTOM>]                                               ;
         [<custom:  EMPTY>]                                                ;
      => ordCondSet( <"for">, <{for}>,                                     ;
                     if( <.all.>, .t., NIL ),                              ;
                     <{while}>,                                            ;
                     <{eval}>, <every>,                                    ;
                     RECNO(), <next>, <rec>,                               ;
                     if( <.rest.>, .t., NIL ),                             ;
                     if( (<.descend.> .AND. !<.ascend.>), .t., NIL ),      ;
                     .f., NIL, .t., <.custom.>, <.non.>, <.add.>, NIL,     ;
                     <"while"> )                                           ;
       ; dbCreateIndex( <(file)>, <"key">, <{key}>,                        ;
                        if( <.unique.>, .t., NIL )  )

#command SUBINDEX ON <key> TAG <(tag)>                                     ;
         [OF <(bag)>]                                                      ;
         [TO <(bag)>]                                                      ;
         [FOR        <for>]                                                ;
         [<all:      ALL>]                                                 ;
         [WHILE      <while>]                                              ;
         [NEXT       <next>]                                               ;
         [RECORD     <rec>]                                                ;
         [<rest:     REST>]                                                ;
         [EVAL       <eval> [EVERY  <every>]]                              ;
         [OPTION     <eval> [STEP   <every>]]                              ;
         [<ascend:   ASCENDING>]                                           ;
         [<descend:  DESCENDING>]                                          ;
         [<unique:   UNIQUE>]                                              ;
         [<add:      ADDITIVE>]                                            ;
         [<custom:   CUSTOM>]                                              ;
         [<custom:   EMPTY>]                                               ;
      => ordCondSet( <"for">, <{for}>,                                     ;
                     if( <.all.>, .t., NIL ),                              ;
                     <{while}>,                                            ;
                     <{eval}>, <every>,                                    ;
                     RECNO(), <next>, <rec>,                               ;
                     if( <.rest.>, .t., NIL ),                             ;
                     if( (<.descend.> .AND. !<.ascend.>), .t., NIL ),      ;
                     .t., <(bag)>, .t., <.custom.>, .f., <.add.>, NIL,     ;
                     <"while"> )                                           ;
       ; ordCreate( <(bag)>, <(tag)>, <"key">, <{key}>, [<.unique.>] )


#command REINDEX                                                           ;
         [EVAL      <eval> [EVERY  <every>]]                               ;
         [OPTION    <eval> [STEP   <every>]]                               ;
      => ordCondSet(,,,, <{eval}>, <every>,,,,,,,,,,,,,)                   ;
       ; ordListRebuild()


#command DELETE TAG <(tag1)> [OF <(bag1)>]                                 ;
         [, <(tagn)> [OF <(bagn)>]]                                        ;
      => ordDestroy( <(tag1)>, <(bag1)> )                                  ;
      [; ordDestroy( <(tagn)>, <(bagn)> )]

#command DELETE TAG <(tag1)> [IN <(bag1)>]                                 ;
         [, <(tagn)> [IN <(bagn)>]]                                        ;
      => ordDestroy( <(tag1)>, <(bag1)> )                                  ;
      [; ordDestroy( <(tagn)>, <(bagn)> )]

#command DELETE TAG ALL                                                    ;
         [OF <(bag)>]                                                      ;
         [IN <(bag)>]                                                      ;
      => if ( AX_Driver() )                                                ;
       ;    AX_KillTag( .t., <(bag)> )                                     ;
       ; else                                                              ;
       ;    SX_KillTag( .t., <(bag)> )                                     ;
       ; end


#command CLEAR SCOPE                                                       ;
      => if ( AX_Driver() )                                                ;
       ;    AX_ClrScope( 0 )                                               ;
       ;    AX_ClrScope( 1 )                                               ;
       ; else                                                              ;
       ;    SX_ClrScope( 0 )                                               ;
       ;    SX_ClrScope( 1 )                                               ;
       ; end

#xcommand SET SCOPETOP TO <value>                                          ;
      => if ( AX_Driver() )                                                ;
       ;    AX_SetScope( 0, <value> )                                      ;
       ; else                                                              ;
       ;    SX_SetScope( 0, <value> )                                      ;
       ; end

#xcommand SET SCOPETOP TO                                                  ;
      => if ( AX_Driver() )                                                ;
       ;    AX_ClrScope( 0 )                                               ;
       ; else                                                              ;
       ;    SX_ClrScope( 0 )                                               ;
       ; end

#xcommand SET SCOPEBOTTOM TO <value>                                       ;
      => if ( AX_Driver() )                                                ;
       ;    AX_SetScope( 1, <value> )                                      ;
       ; else                                                              ;
       ;    SX_SetScope( 1, <value> )                                      ;
       ; end

#xcommand SET SCOPEBOTTOM TO                                               ;
      => if ( AX_Driver() )                                                ;
       ;    AX_ClrScope( 1 )                                               ;
       ; else                                                              ;
       ;    SX_ClrScope( 1 )                                               ;
       ; end

#command SET SCOPE TO                                                      ;
      => if ( AX_Driver() )                                                ;
       ;    AX_ClrScope( 0 )                                               ;
       ;    AX_ClrScope( 1 )                                               ;
       ; else                                                              ;
       ;    SX_ClrScope( 0 )                                               ;
       ;    SX_ClrScope( 1 )                                               ;
       ; end

#command SET SCOPE TO <value>                                              ;
      => if ( AX_Driver() )                                                ;
       ;    AX_SetScope( 0, <value> )                                      ;
       ;    AX_SetScope( 1, <value> )                                      ;
       ; else                                                              ;
       ;    SX_SetScope( 0, <value> )                                      ;
       ;    SX_SetScope( 1, <value> )                                      ;
       ; end


#command SET MEMOBLOCK TO <value>                                          ;
      =>    AX_SetMemoBlock( <value> )                                     ;
       ;    SX_SetMemoBlock( <value> )                                     

#command SORT [TO <(file)>] [ON <fields,...>]                              ;
         [FOR    <for>]                                                    ;
         [WHILE  <while>]                                                  ;
         [NEXT   <next>]                                                   ;
         [RECORD <rec>]                                                    ;
         [<rest: REST>]                                                    ;
         [ALL]                                                             ;
         [<cur:  USECURRENT>]                                              ;
                                                                           ;
      => if ( AX_Driver() )                                                ;
       ;    AX_SortOption(<.cur.>)                                         ;
       ; else                                                              ;
       ;    SX_SortOption(<.cur.>)                                         ;
       ; end                                                               ;
       ; __dbSort( <(file)>, { <(fields)> },                               ;
                   <{for}>, <{while}>, <next>, <rec>, <.rest.> )

#command AUTOUSE <(db)> VIA <rdd> ALTERNATE <altrdd>                       ;
         [ALIAS <a>]                                                       ;
         [<new: NEW>]                                                      ;
         [<ex: EXCLUSIVE>]                                                 ;
         [<sh: SHARED>]                                                    ;
         [<ro: READONLY>]                                                  ;
         [INDEX <(index1)> [, <(indexn)>]]                                 ;
         [PASSWORD <(password)>]                                           ;
                                                                           ;
      => if AX_Loaded( AX_GetDrive( <(db)> ) )                             ;
       ;    dbUseArea(                                                     ;
                      <.new.>, <rdd>, <(db)>, <(a)>,                       ;
                      if (<.sh.> .or. <.ex.>, !<.ex.>, NIL), <.ro.>        ;
                     )                                                     ;
            [; AX_SetPass( <(password)> )]                                 ;
            [; dbSetIndex( <(index1)> )]                                   ;
            [; dbSetIndex( <(indexn)> )]                                   ;
       ; else                                                              ;
       ;    dbUseArea(                                                     ;
                      <.new.>, <altrdd>, <(db)>, <(a)>,                    ;
                      if (<.sh.> .or. <.ex.>, !<.ex.>, NIL), <.ro.>        ;
                     )                                                     ;
            [; SX_SetPass( <(password)> )]                                 ;
            [; dbSetIndex( <(index1)> )]                                   ;
            [; dbSetIndex( <(indexn)> )]                                   ;
       ; endif

/* Redefinition of USE for PASSWORDs */
#command USE <(db)>                                                        ;
             [VIA <rdd>]                                                   ;
             [ALIAS <a>]                                                   ;
             [<new: NEW>]                                                  ;
             [<ex: EXCLUSIVE>]                                             ;
             [<sh: SHARED>]                                                ;
             [<ro: READONLY>]                                              ;
             [INDEX <(index1)> [, <(indexn)>]]                             ;
             [PASSWORD <(password)>]                                       ;
                                                                           ;
      => dbUseArea(                                                        ;
                    <.new.>, <rdd>, <(db)>, <(a)>,                         ;
                    if(<.sh.> .or. <.ex.>, !<.ex.>, NIL), <.ro.>           ;
                  )                                                        ;
                                                                           ;
      [; AX_SetPass( <(password)> )]                                       ;
      [; dbSetIndex( <(index1)> )]                                         ;
      [; dbSetIndex( <(indexn)> )]
