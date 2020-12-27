
export const bc_homePage = () => [
    {
        title: 'Главная',
        to: { name: 'homePage' },
        link: true
    }
];

export const bc_error404 = () => [...bc_homePage(), {
    title: 'Ошибка 404',
    link: false
}];

export const bc_subjectsList = () => [...bc_homePage(), {
    title: 'Предметы',
    to: { name: 'subjectsList' },
    link: true
}];

export const bc_subjectView = (prms: {id: string, name: string}) => [...bc_subjectsList(), {
    title: prms.name,
    to: { name: 'subjectView', params: { id: prms.id }},
    link: true
}];

export const bc_lessonView = (subjPrms: {
        id: string,
        name: string
      },
  lessonPrms: {
    id: string,
    name: string
}) => [
    ...bc_subjectView(subjPrms),
    {
        title: lessonPrms.name,
        to: { name: 'lessonView', params: { lessonId: lessonPrms.id } },
        link: true
    }
];

export const bc_calendar = () => [...bc_homePage(), {
    title: "Календарь",
    to: { name: "calendarView" },
    link: true
}];
