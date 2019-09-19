insert into GatePass(usr,pss,Img) SELECT 'Danish','Danish',* FROM Openrowset( Bulk 'D:\My Files\CMA\PP---Danish---L.jpg', Single_Blob) Image;

