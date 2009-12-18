// WINDOWS.CH   DE SPENCE
#define IKEY_VAL(m_name) pad(upper(m_name), len(menu_name))

#define COMPILE(c_b)          &( "{||" + c_b + "}" )

#define BLK(c_b)              &( "{||" + c_b + "}" )

#define BLK_VAL(c_b,p_m)      &( "{|" + p_m + "|" + c_b + "}" )

#define P_DEF(pn, def)        pn := iif(pn == NIL, def, pn)

#define P_DEFVAL(pn, def)     iif(pn == NIL, def, pn)

#define ASSERT(expl,func)     iif(expl," ",func(procline()))

#define LEN_COL_STR           20 
#define LEN_VID_STK_ENTRY     LEN_COL_STR + 3
#define LEN_DBF_STK_ENTRY     11

#define GET_T(win_str)        asc(substr(win_str,1,1))

#define GET_L(win_str)        asc(substr(win_str,2,1))

#define GET_B(win_str)        asc(substr(win_str,3,1))

#define GET_R(win_str)        asc(substr(win_str,4,1))

#define COORD_SIZE            4

#define GET_WIN(win_str)      substr(win_str, COORD_SIZE + 1)

#define ATTR_CONV(FORE,BACK)  (FORE) * 16 + (BACK)

#define BLACK                 0
#define WHITE                 7
#define DK_GRAY               8

#define COL_SHADOW_ATTRIBUTE  ATTR_CONV(DK_GRAY,BLACK)
#define BW_SHADOW_ATTRIBUTE   ATTR_CONV(WHITE  ,BLACK)

#define OK_BUTTON             1
#define YES_BUTTON            1
#define NO_BUTTON             2
#define CANCEL_BUTTON         3

#define SB_IND_CHAR           219
#define SB_BACK_CHAR          177
