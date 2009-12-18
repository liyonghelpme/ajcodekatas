/*
 * Source     : DBFNTXAX.CH for CA-Clipper 5.3
 * Description: Header file for the Advantage NTX RDD (DBFNTXAX).
 *              Include this file in your application if you want to use
 *              any of the commands defined below with the Advantage NTX
 *              RDD (DBFNTXAX).  To make DBFNTXAX the default RDD in your
 *              application, link DBFNTXAX.OBJ into your application.
 *              If you link DBFNTXAX.OBJ into your application, and you
 *              want to use both DBFNTXAX and DBFNTX in the same
 *              application, then you must also add the statement
 *              'REQUEST DBFNTX' somewhere in your code.
 *
 * Copyright 1994-1998 - Extended Systems, Inc.
 */

REQUEST DBFNTXAX


#command APPEND [FROM <(file)>]                                            ;
         [VIA <rdd>]                                                       ;
         [ALL]                                                             ;
      => AX_AppendFrom( <(file)>, <rdd> )

#command COPY [TO <(file)>]                                                ;
         [VIA <rdd>]                                                       ;
         [ALL]                                                             ;
      => AX_CopyTo( <(file)>, <rdd> )

#command SET PASSWORD TO <(password)>                                      ;
      => AX_SetPass( <(password)>, .T. )

#command SET PASSWORD TO                                                   ;
      => AX_SetPass( "", .T. )

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
      => ordCondSet( <"for">, <{for}>,                                     ;
                     [<.all.>],                                            ;
                     <{while}>,                                            ;
                     <{eval}>, <every>,                                    ;
                     RECNO(), <next>, <rec>,                               ;
                     [<.rest.>], [<.descend.>],                            ;
                     NIL, <.add.>, <.cur.>, .f., NIL,                      ;
                     .f., NIL, <"while">, .f. )                            ;
       ; dbCreateIndex( <(file)>, <"key">, <{key}>, [<.unique.>] )

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
         [<add:     ADDITIVE>]                                             ;
      => ordCondSet( <"for">, <{for}>,                                     ;
                     [<.all.>],                                            ;
                     <{while}>,                                            ;
                     <{eval}>, <every>,                                    ;
                     RECNO(), <next>, <rec>,                               ;
                     [<.rest.>], [<.descend.>],                            ;
                     NIL, <.add.>, .t., .f., NIL,                          ;
                     .f., NIL, <"while">, .f. )                            ;
       ; dbCreateIndex( <(file)>, <"key">, <{key}>, [<.unique.>] )


#command REINDEX                                                           ;
         [EVAL       <eval> [EVERY  <every>]]                              ;
         [OPTION     <eval> [STEP   <every>]]                              ;
       =>  ordCondSet(,,,, <{eval}>, <every>,,,,,,,,,,,,,)                 ;
         ; ordListRebuild()


#command CLEAR SCOPE                                                       ;
      => ordScope( 0, NIL )                                                ;
       ; ordScope( 1, NIL )

#xcommand SET SCOPETOP TO <value>                                          ;
      => ordScope( 0, <value> )

#xcommand SET SCOPETOP TO                                                  ;
      => ordScope( 0, NIL )

#xcommand SET SCOPEBOTTOM TO <value>                                       ;
      => ordScope( 1, <value> )

#xcommand SET SCOPEBOTTOM TO                                               ;
      => ordScope( 1, NIL )

#command SET SCOPE TO                                                      ;
      => ordScope( 0, NIL )                                                ;
       ; ordScope( 1, NIL )

#command SET SCOPE TO <value>                                              ;
      => ordScope( 0, <value> )                                            ;
       ; ordScope( 1, <value> )

#command SORT [TO <(file)>] [ON <fields,...>]                              ;
         [FOR    <for>]                                                    ;
         [WHILE  <while>]                                                  ;
         [NEXT   <next>]                                                   ;
         [RECORD <rec>]                                                    ;
         [<rest: REST>]                                                    ;
         [ALL]                                                             ;
         [<cur:  USECURRENT>]                                              ;
                                                                           ;
      => AX_SortOption( <.cur.> )                                          ;
       ; __dbSort( <(file)>, { <(fields)> },                               ;
                   <{for}>, <{while}>, <next>, <rec>, <.rest.> )

#command AUTOUSE <(db)> VIA <rdd> ALTERNATE <altrdd>                       ;
         [ALIAS <a>]                                                       ;
         [<new: NEW>]                                                      ;
         [<ex: EXCLUSIVE>]                                                 ;
         [<sh: SHARED>]                                                    ;
         [<ro: READONLY>]                                                  ;
         [INDEX <(index1)> [, <(indexn)>]]                                 ;
                                                                           ;
      => if AX_Loaded( AX_GetDrive( <(db)> ) )                             ;
       ;    dbUseArea(                                                     ;
                      <.new.>, <rdd>, <(db)>, <(a)>,                       ;
                      if (<.sh.> .or. <.ex.>, !<.ex.>, NIL), <.ro.>        ;
                     )                                                     ;
            [; dbSetIndex( <(index1)> )]                                   ;
            [; dbSetIndex( <(indexn)> )]                                   ;
       ; else                                                              ;
       ;    dbUseArea(                                                     ;
                      <.new.>, <altrdd>, <(db)>, <(a)>,                    ;
                      if (<.sh.> .or. <.ex.>, !<.ex.>, NIL), <.ro.>        ;
                     )                                                     ;
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
      [; AX_SetPass( <(password)>, .T. )]                                  ;
      [; dbSetIndex( <(index1)> )]                                         ;
      [; dbSetIndex( <(indexn)> )]


/*
 * Constants for AX_GetServerType()
 */
#define ADS_MGMT_NETWARE_SERVER        1
#define ADS_MGMT_NT_SERVER             2
#define ADS_MGMT_LOCAL_SERVER          3

/*
 * Constants for AX_LockOwner()
 */
#define ADS_MGMT_NO_LOCK               1
#define ADS_MGMT_RECORD_LOCK           2
#define ADS_MGMT_FILE_LOCK             3

/*
 * Constants for AX_OpenTables()
 */
#define ADS_MGMT_PROPRIETARY_LOCKING   1
#define ADS_MGMT_CDX_LOCKING           2
#define ADS_MGMT_NTX_LOCKING           3
