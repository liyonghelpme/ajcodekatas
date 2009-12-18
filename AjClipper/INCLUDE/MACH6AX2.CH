/*
 * Source file: MACH6AX2.CH (CA-Clipper 5.2)
 * Description: Header file for the Advantage Mach6Ax query optimizer.
 *              Include this file (#include "MACH6AX2.CH") in your
 *              application if you want to use the Advantage Mach6Ax
 *              query optimization features.
 * Note       : Use this header file, rather than MACH6AX2.CH, if you are
 *              using extended expressions in your optimizable Mach6Ax
 *              commands.  See the #command smart stringify result-marker
 *              section in your CA-Clipper manual for an explanation of
 *              extended expressions.
 * Notice     : Copyright 1993-1996 - Extended Systems, Inc.
 */

EXTERN M6AXINIT

#translate NO OPTIMIZE  =>  NOOPTIMIZE

//=============================================================================
// RETURN VALUES FROM A6_ISOPTIMIZE()
//-----------------------------------------------------------------------------

#define OPT_FULL          2
#define OPT_PARTIAL       1
#define OPT_NONE          0


//=============================================================================
// A6_SET() DEFINITIONS
//-----------------------------------------------------------------------------

#define _SET_TYPECHECK    1
#define _SET_OPTIMIZE     2


//=============================================================================
// SPECIFY "NOOPTIMIZE" TO OVERRIDE AUTOMATIC OPTIMIZATION
//-----------------------------------------------------------------------------

#command AVERAGE [ <x1> [, <xn>]  TO  <v1> [, <vn>] ]                   ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => M->__Avg := <v1> := [ <vn> := ] 0                              ;
       ; DBEval( { || M->__Avg := M->__Avg + 1,                         ;
                 <v1> := <v1> + <x1> [, <vn> := <vn> + <xn>] },         ;
                 <{for}>, <{while}>, <next>, <rec>, <.rest.> )          ;
       ; <v1> := <v1> / M->__Avg [; <vn> := <vn> / M->__Avg ]


#command COUNT [TO <var>]                                               ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => <var> := 0                                                     ;
       ; DBEval( { || <var> := <var> + 1 },                             ;
                 <{for}>, <{while}>, <next>, <rec>, <.rest.> )


#command COPY [TO <(file)>]                                             ;
         [FIELDS <fields,...>]                                          ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
         [DELIMITED [WITH <*delim*>]]                                   ;
                                                                        ;
      => __dbDelim( .T.,                                                ;
                    <(file)>, <(delim)>, { <(fields)> },                ;
                    <{for}>, <{while}>, <next>, <rec>, <.rest.> )


#command COPY [TO <(file)>] [SDF]                                       ;
         [FIELDS <fields,...>]                                          ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => __dbSDF( .T.,                                                  ;
                  <(file)>, { <(fields)> },                             ;
                  <{for}>, <{while}>, <next>, <rec>, <.rest.> )


#command COPY [TO <(file)>]                                             ;
         [FIELDS <fields,...>]                                          ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [VIA <rdd>]                                                    ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => __dbCopy( <(file)>, { <(fields)> },                            ;
                   <{for}>, <{while}>, <next>, <rec>, <.rest.>, <rdd> )


#command DELETE                                                         ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => DBEval( { || dbDelete() },                                     ;
                 <{for}>, <{while}>, <next>, <rec>, <.rest.> )


#command DISPLAY [<list,...>]                                           ;
         [<off:OFF>]                                                    ;
         [<toPrint: TO PRINTER>]                                        ;
         [TO FILE <(toFile)>]                                           ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [<all:ALL>]                                                    ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => __DBList( <.off.>, { <{list}> }, <.all.>,                      ;
                   <{for}>, <{while}>, <next>, <rec>, <.rest.>,         ;
                   <.toPrint.>, <(toFile)> )


#command LABEL FORM <lbl>                                               ;
         [<sample: SAMPLE>]                                             ;
         [<noconsole: NOCONSOLE>]                                       ;
         [<print: TO PRINTER>]                                          ;
         [TO FILE <(toFile)>]                                           ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => __LabelForm( <(lbl)>, <.print.>, <(toFile)>, <.noconsole.>,    ;
                      <{for}>, <{while}>, <next>, <rec>, <.rest.>,      ;
                      <.sample.> )


#command LIST [<list,...>]                                              ;
         [<off:OFF>]                                                    ;
         [<toPrint: TO PRINTER>]                                        ;
         [TO FILE <(toFile)>]                                           ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => __dbList( <.off.>, { <{list}> }, .t.,                          ;
                   <{for}>, <{while}>, <next>, <rec>, <.rest.>,         ;
                   <.toPrint.>, <(toFile)> )


#command REPLACE [ <f1> WITH <x1> [, <fn> WITH <xn>] ]                  ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => DBEval( {|| _FIELD-><f1> := <x1> [, _FIELD-><fn> := <xn>]},    ;
                 <{for}>, <{while}>, <next>, <rec>, <.rest.> )


#command REPORT FORM <frm>                                              ;
         [HEADING <heading>]                                            ;
         [<plain: PLAIN>]                                               ;
         [<noeject: NOEJECT>]                                           ;
         [<summary: SUMMARY>]                                           ;
         [<noconsole: NOCONSOLE>]                                       ;
         [<print: TO PRINTER>]                                          ;
         [TO FILE <(toFile)>]                                           ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => __ReportForm( <(frm)>, <.print.>, <(toFile)>, <.noconsole.>,   ;
                       <{for}>, <{while}>, <next>, <rec>, <.rest.>,     ;
                       <.plain.>, <heading>, <.noeject.>, <.summary.> )


#command SET FILTER TO <xpr>                                            ;
         [NOOPTIMIZE]                                                   ;
      => dbSetFilter( <{xpr}>, <"xpr"> )


#command SET FILTER TO <x:&>                                            ;
         [NOOPTIMIZE]                                                   ;
      => if ( Empty(<(x)>) )                                            ;
       ;    dbClearFilter()                                             ;
       ; else                                                           ;
       ;    dbSetFilter( <{x}>, <(x)> )                                 ;
       ; endif


#command SUM [ <x1> [, <xn>]  TO  <v1> [, <vn>] ]                       ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => <v1> := [ <vn> := ] 0                                          ;
       ; DBEval( { || <v1> := <v1> + <x1> [, <vn> := <vn> + <xn> ] },   ;
                 <{for}>, <{while}>, <next>, <rec>, <.rest.> )


#command SORT [TO <(file)>] [ON <fields,...>]                           ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => __dbSort( <(file)>, { <(fields)> },                            ;
                   <{for}>, <{while}>, <next>, <rec>, <.rest.> )


#command TOTAL [TO <(file)>] [ON <key>]                                 ;
         [FIELDS <fields,...>]                                          ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => __dbTotal( <(file)>, <{key}>, { <(fields)> },                  ;
                    <{for}>, <{while}>, <next>, <rec>, <.rest.> )


#command RECALL                                                         ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [NOOPTIMIZE]                                                   ;
                                                                        ;
      => DBEval( { || dbRecall() },                                     ;
                 <{for}>, <{while}>, <next>, <rec>, <.rest.> )


//=============================================================================
// MACH6AX OPTIMIZED COMMANDS
//-----------------------------------------------------------------------------

#command AVERAGE [ <x1> [, <xn>]  TO  <v1> [, <vn>] ]                   ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
                                                                        ;
      => M->__Avg := <v1> := [ <vn> := ] 0                              ;
       ; A6_dbEval( { || M->__Avg := M->__Avg + 1,                      ;
                    <v1> := <v1> + <x1> [, <vn> := <vn> + <xn>] },      ;
                    <(for)>, NIL)                                       ;
       ; <v1> := <v1> / M->__Avg [; <vn> := <vn> / M->__Avg ]


#command COPY [TO <(file)>] [FIELDS <fields,...>]                       ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
         [DELIMITED [WITH <*delim*>]]                                   ;
                                                                        ;
      => a6_CopyDelim( <(file)>, <(delim)>, { <(fields)> },             ;
                      <(for)>, NIL )


#command COPY [TO <(file)>] [SDF]                                       ;
         [FIELDS <fields,...>]                                          ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
                                                                        ;
      => a6_CopySdf( <(file)>, { <(fields)> }, <(for)>, NIL )


#command COPY [TO <(file)>]                                             ;
         [FIELDS <fields,...>]                                          ;
         [FOR <for>]                                                    ;
         [VIA <rdd>]                                                    ;
         [ALL]                                                          ;
                                                                        ;
      => a6_Copy( <(file)>, { <(fields)> }, <(for)>, NIL, <rdd> )


#command COPY TO ARRAY <var>                                            ;
         [FIELDS <fields,...>]                                          ;
         [FOR <for>]                                                    ;
         [WHILE <while>]                                                ;
         [NEXT <next>]                                                  ;
         [RECORD <rec>]                                                 ;
         [<rest:REST>]                                                  ;
         [ALL]                                                          ;
         [<x:OFF>]                                                      ;
                                                                        ;
      => <var>:={}                                                      ;
       ; a6_CopyToArray( @<var>, { <(fields)> }, <(for)>, NIL,          ;
                         <{while}>, <next>, <rec>, <.rest.>, !<.x.> )   ;


#command COUNT [TO <var>] [FOR <for>] [ALL]                             ;
      => <var> := a6_CountFor( <(for)>, NIL )


#command DELETE [FOR <for>] [ALL]                                       ;
      => A6_dbEval( { || dbDelete() }, <(for)>, NIL )


#command DELETE                                                         ;
      => dbDelete()


#command DISPLAY [<list,...>]                                           ;
         [<off:OFF>]                                                    ;
         [<toPrint: TO PRINTER>]                                        ;
         [TO FILE <(toFile)>]                                           ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
                                                                        ;
      => a6_ListFor( <.off.>, { <{list}> }, .T.,                        ;
                    <(for)>, NIL, <.toPrint.>, <(toFile)> )


#command INDEX ON <key> TO <(file)>                                     ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
         [ASCENDING]                                                    ;
         [<dec:    DESCENDING>]                                         ;
         [<u:      UNIQUE>]                                             ;
         [<cur:    USECURRENT>]                                         ;
         [<cur:    SUBINDEX>]                                           ;
         [EVAL     <opt> [EVERY <step>]]                                ;
         [OPTION   <opt> [STEP <step>]]                                 ;
         [<non:    NONCOMPACT>]                                         ;
         [<add:    ADDITIVE>]                                           ;
         [<custom: CUSTOM>]                                             ;
         [<custom: EMPTY>]                                              ;
                                                                        ;
      => a6_ordCondSet( <(for)>, NIL, NIL, NIL, <{opt}>,                ;
                        <step>, RECNO(), NIL, NIL, NIL, [<.dec.>],      ;
                        .F., NIL, <.cur.>, <.custom.>, <.non.>, <.add.>,;
                        NIL, NIL )                                      ;
       ; a6_CreateIndex( <(file)>, <(key)>, NIL, [<.u.>] )


#command INDEX ON <key> TAG <(tag)>                                     ;
         [OF <(cdx)>]                                                   ;
         [TO <(cdx)>]                                                   ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
         [ASCENDING]                                                    ;
         [<dec:    DESCENDING>]                                         ;
         [<u:      UNIQUE>]                                             ;
         [<cur:    USECURRENT>]                                         ;
         [<cur:    SUBINDEX>]                                           ;
         [EVAL     <opt> [EVERY <step>]]                                ;
         [OPTION   <opt> [STEP <step>]]                                 ;
         [<add:    ADDITIVE>]                                           ;
         [<custom: CUSTOM>]                                             ;
         [<custom: EMPTY>]                                              ;
                                                                        ;
      => a6_ordCondSet( <(for)>, NIL, NIL, NIL, <{opt}>,                ;
                        <step>, RECNO(), NIL, NIL, NIL, [<.dec.>],      ;
                        .T., <(cdx)>, <.cur.>, <.custom.>, NIL, <.add.>,;
                        NIL, NIL )                                      ;
       ; a6_ordCreate( <(cdx)>, <(tag)>, <(key)>, NIL, [<.u.>] )


#command LABEL FORM <lbl>                                               ;
         [<sample: SAMPLE>]                                             ;
         [<noconsole: NOCONSOLE>]                                       ;
         [<print: TO PRINTER>]                                          ;
         [TO FILE <(toFile)>]                                           ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
                                                                        ;
      => a6_LabelForm( <(lbl)>, <.print.>, <(toFile)>, <.noconsole.>,   ;
                       <(for)>, NIL, <.sample.> )


#command LIST [<list,...>]                                              ;
         [<off:OFF>]                                                    ;
         [<toPrint: TO PRINTER>]                                        ;
         [TO FILE <(toFile)>]                                           ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
                                                                        ;
      => a6_ListFor( <.off.>, { <{list}> }, .t.,                        ;
                     <(for)>, NIL, <.toPrint.>, <(toFile)> )


#command RECALL [FOR <for>] [ALL]                                       ;
      => A6_dbEval( { || dbRecall() }, <(for)>, NIL )


#command RECALL                                                         ;
      => dbRecall()


#command REFRESH FILTER                                                 ;
      => a6_RefreshFilter()


#command REPLACE [ <f1> WITH <x1> [, <fn> WITH <xn>] ]                  ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
                                                                        ;
      => A6_dbEval( {|| _FIELD-><f1> := <x1> [, _FIELD-><fn> := <xn>]}, ;
                    <(for)>, NIL )


#command REPLACE <f1> WITH <v1> [, <fN> WITH <vN> ]                     ;
      => _FIELD-><f1> := <v1> [; _FIELD-><fN> := <vN>]


#command REPORT FORM <frm>                                              ;
         [HEADING <heading>]                                            ;
         [<plain: PLAIN>]                                               ;
         [<noeject: NOEJECT>]                                           ;
         [<summary: SUMMARY>]                                           ;
         [<noconsole: NOCONSOLE>]                                       ;
         [<print: TO PRINTER>]                                          ;
         [TO FILE <(toFile)>]                                           ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
                                                                        ;
      => a6_ReportForm( <(frm)>, <.print.>, <(toFile)>, <.noconsole.>,  ;
                        <(for)>, NIL, <.plain.>, <heading>,             ;
                        <.noeject.>, <.summary.> )


#command SEARCH [FOR <for>] [TO <var>]                                  ;
      => <var>:={}                                                      ;
       ; a6_Search( <(for)>, NIL, @<var> )


#command SET FILTER TO <xpr>                                            ;
      => a6_SetFilter( <{xpr}>, <(xpr)>, .F. )


#command SET FILTER TO <x:&>                                            ;
      => if ( Empty(<(x)>) )                                            ;
       ;    dbClearFilter()                                             ;
       ; else                                                           ;
       ;    a6_SetFilter( <{x}>, <(x)>, .F. )                           ;
       ; endif


#command SUBINDEX ON <key> TO <(file)>                                  ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
         [ASCENDING]                                                    ;
         [<dec:    DESCENDING>]                                         ;
         [<u:      UNIQUE>]                                             ;
         [EVAL     <opt> [EVERY <step>]]                                ;
         [OPTION   <opt> [STEP <step>]]                                 ;
         [<non:    NONCOMPACT>]                                         ;
         [<add:    ADDITIVE>]                                           ;
         [<custom: CUSTOM>]                                             ;
         [<custom: EMPTY>]                                              ;
                                                                        ;
      => a6_ordCondSet( <(for)>, NIL, NIL, NIL, <{opt}>,                ;
                        <step>, RECNO(), NIL, NIL, NIL, [<.dec.>],      ;
                        .F., NIL, .T., <.custom.>, <.non.>, <.add.>,    ;
                        NIL, NIL )                                      ;
       ; a6_CreateIndex( <(file)>, <(key)>, NIL, [<.u.>] )


#command SUBINDEX ON <key> TAG <(tag)>                                  ;
         [OF <(cdx)>]                                                   ;
         [TO <(cdx)>]                                                   ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
         [ASCENDING]                                                    ;
         [<dec:    DESCENDING>]                                         ;
         [<u:      UNIQUE>]                                             ;
         [EVAL     <opt> [EVERY <step>]]                                ;
         [OPTION   <opt> [STEP <step>]]                                 ;
         [<add:    ADDITIVE>]                                           ;
         [<custom: CUSTOM>]                                             ;
         [<custom: EMPTY>]                                              ;
                                                                        ;
      => a6_ordCondSet( <(for)>, NIL, NIL, NIL, <{opt}>,                ;
                        <step>, RECNO(), NIL, NIL, NIL, [<.dec.>],      ;
                        .T., <(cdx)>, .T., <.custom.>, NIL, <.add.>,    ;
                        NIL, NIL )                                      ;
       ; a6_ordCreate( <(cdx)>, <(tag)>, <(key)>, NIL, [<.u.>] )


#command SUM [ <x1> [, <xn>]  TO  <v1> [, <vn>] ]                       ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
                                                                        ;
      => <v1> := [ <vn> := ] 0                                          ;
       ; A6_dbEval( {|| <v1> := <v1> + <x1> [, <vn> := <vn> + <xn> ]},  ;
                    <(for)>, NIL )


#command SORT [TO <(file)>] [ON <fields,...>]                           ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
                                                                        ;
      => a6_SortFor( <(file)>, { <(fields)> }, <(for)>, NIL )


#command TOTAL [TO <(file)>] [ON <key>]                                 ;
         [FIELDS <fields,...>]                                          ;
         [FOR <for>]                                                    ;
         [ALL]                                                          ;
                                                                        ;
      => a6_TotalFor( <(file)>, <{key}>, { <(fields)> },                ;
                      <(for)>, NIL )


#command SET TYPECHECK <x:ON,OFF,&>                                     ;
      => a6_Set( _SET_TYPECHECK, <(x)> )


#command SET TYPECHECK (<x>)                                            ;
      => a6_Set( _SET_TYPECHECK, <x> )


#command SET OPTIMIZE <x:ON,OFF,&>                                      ;
      => a6_Set( _SET_OPTIMIZE, <(x)> )


#command SET OPTIMIZE (<x>)                                             ;
      => a6_Set( _SET_OPTIMIZE, <x> )
