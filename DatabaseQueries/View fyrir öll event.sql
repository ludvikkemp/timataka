
select e.Id as 'EventID', e.Name as 'EventName', c.Name as 'InstanceName', d.Name as 'Discipline', s.Name as 'Sport'
from Events e
inner join CompetitionInstances c on e.CompetitionInstanceId = c.Id
inner join Disciplines d on e.DisciplineId = d.Id
inner join Sports s on d.SportId = s.Id;
