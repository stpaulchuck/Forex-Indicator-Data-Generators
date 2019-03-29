use forexswing;

select * from audusd1440 as a join ccidata as b on a.bartime = b.bartime 
where b.pairname = 'audusd' and period = 1440
order by a.bartime asc;
