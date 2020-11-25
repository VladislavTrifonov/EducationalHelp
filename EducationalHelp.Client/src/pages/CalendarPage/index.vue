<template>
  <div data-app style="width: 100%">
    <v-app>
      <v-row class="fill-height">
        <v-col>
          <v-row>
            <v-col cols="auto">
              <a href="https://localhost:5001/calendar/ics"><v-btn outlined>Экспортировать в .ics</v-btn></a>
            </v-col>
          </v-row>
          <v-sheet height="64">
            <v-toolbar
                flat
            >
              <v-btn
                  outlined
                  class="mr-4"
                  color="grey darken-2"
                  @click="setToday"
              >
                Сегодня
              </v-btn>
              <v-btn
                  fab
                  text
                  small
                  color="grey darken-2"
                  @click="prev"
              >
                <v-icon small>
                  mdi-chevron-left
                </v-icon>
              </v-btn>
              <v-btn
                  fab
                  text
                  small
                  color="grey darken-2"
                  @click="next"
              >
                <v-icon small>
                  mdi-chevron-right
                </v-icon>
              </v-btn>
              <v-toolbar-title v-if="$refs.calendar">
                {{ $refs.calendar.title }}
              </v-toolbar-title>
              <v-spacer></v-spacer>
              <v-menu
                  bottom
                  right
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                      outlined
                      color="grey darken-2"
                      v-bind="attrs"
                      v-on="on"
                  >
                    <span>{{ typeToLabel[type] }}</span>
                    <v-icon right>
                      mdi-menu-down
                    </v-icon>
                  </v-btn>
                </template>
                <v-list>
                  <v-list-item @click="type = 'day'">
                    <v-list-item-title>День</v-list-item-title>
                  </v-list-item>
                  <v-list-item @click="type = 'week'">
                    <v-list-item-title>Неделя</v-list-item-title>
                  </v-list-item>
                  <v-list-item @click="type = 'month'">
                    <v-list-item-title>Месяц</v-list-item-title>
                  </v-list-item>
                  <v-list-item @click="type = '4day'">
                    <v-list-item-title>4 дня</v-list-item-title>
                  </v-list-item>
                </v-list>
              </v-menu>
            </v-toolbar>
          </v-sheet>
          <v-sheet height="600">
            <v-calendar
                ref="calendar"
                v-model="focus"
                color="primary"
                :events="events"
                :event-color="getEventColor"
                :type="type"
                @click:event="showEvent"
                @click:more="viewDay"
                @click:date="viewDay"
                @change="updateRange"
                :weekdays="weekdays"
            ></v-calendar>
            <v-menu
                v-model="selectedOpen"
                :close-on-content-click="false"
                :activator="selectedElement"
                offset-x
            >
              <v-card
                  color="grey lighten-4"
                  min-width="350px"
                  flat
              >
                <v-toolbar
                    :color="selectedEvent.color"
                    dark
                >
                  <v-toolbar-title>{{selectedEvent.summary}}</v-toolbar-title>
                  <v-spacer></v-spacer>
                  <v-btn
                      icon
                      @click="selectedOpen = false"
                  >
                    <v-icon>mdi-close</v-icon>
                  </v-btn>
                </v-toolbar>
                <v-card-actions>

                    <v-btn
                        text
                        color="primary">
                      Перейти к источнику события
                    </v-btn>

                </v-card-actions>
              </v-card>
            </v-menu>
          </v-sheet>
        </v-col>
      </v-row>
    </v-app>
  </div>
</template>

<script lang="js" >
import {Response} from "@/store/modules/ErrorProcessing";
import CalendarAPI from "@/api/CalendarAPI";

export default {
  data: () => ({
    api: new CalendarAPI(),
    focus: '',
    type: 'month',
    typeToLabel: {
      month: 'Месяц',
      week: 'Неделя',
      day: 'День',
      '4day': '4 дня',
    },
    selectedEvent: {},
    selectedElement: null,
    selectedOpen: false,
    events: [],
    events_cache_minDate: new Date(8640000000000000), // минимальная дата, за которую загружены события в кэш
    events_cache_maxDate: new Date(-8640000000000000),// максимальная дата, за которую загружены события в кэш
    colors: ['blue', 'indigo', 'deep-purple', 'cyan', 'green', 'orange', 'grey darken-1'],
    weekdays: [1, 2, 3, 4, 5, 6, 0]
  }),
  mounted () {
    this.$refs.calendar.checkChange()
  },
  methods: {
    viewDay ({ date }) {
      this.focus = date
      this.type = 'day'
    },
    getEventColor (event) {
      return event.color
    },
    setToday () {
      this.focus = ''
    },
    prev () {
      this.$refs.calendar.prev()
    },
    next () {
      this.$refs.calendar.next()
    },
    showEvent ({ nativeEvent, event }) {
      const open = () => {
        this.selectedEvent = event
        this.selectedElement = nativeEvent.target
        setTimeout(() => {
          this.selectedOpen = true
        }, 10)
      }

      if (this.selectedOpen) {
        this.selectedOpen = false
        setTimeout(open, 10)
      } else {
        open()
      }

      nativeEvent.stopPropagation()
    },
    updateRange ({ start, end }) {
      let startDate = new Date(start.date)
      let endDate = new Date(end.date)

      // Запрошенные события полностью содержатся в кэше, данные можно не запрашивать снова
      if (startDate >= this.events_cache_minDate && endDate <= this.events_cache_maxDate) {
        return
      }

      // Запрошенные события не содержатся, или содержатся, но не полностью в кэше.
      Response.fromPromise(this.api.getEvents(startDate, endDate), (response) => {
        let events = []
        if (this.events_cache_minDate <= startDate) {
          let leftEvents = response.filter(e => new Date(e.dateStart) < startDate);
          events = [...events, ...leftEvents]
        }

        if (this.events_cache_maxDate <= endDate) {
          let rightEvents = response.filter(e => new Date(e.dateStart) < endDate);
          events = [...events, ...rightEvents]
        }
        let eventsSorted = events.sort((a, b) => {
          if (a.dateStart < b.dateStart) {
            return -1;
          } else {
            return 1; // a > b
          }
          return 0; // a == b
        });

        for (let i = 0; i < events.length; i++) {
          events[i] = this.convertToEventObject(events[i])
        }

        this.events = events;

      }).catch(error => {
        console.log("error!", error);
      })
      this.updateMaxCachedValue(endDate)
      this.updateMinCachedValue(startDate)
    },

    updateMinCachedValue(newValue) {
      if (newValue < this.events_cache_minDate) {
        this.events_cache_minDate = newValue;
      }
    },

    updateMaxCachedValue(newValue) {
      if (newValue > this.events_cache_maxDate) {
        this.events_cache_maxDate = newValue
      }
    },

    convertToEventObject(v) {
      v.name = v.summary
      v.start = new Date(v.dateStart)
      v.end = new Date(v.dateEnd)
      v.color = 'blue'
      v.timed = true
      return v
    }


  },
}
</script>


<style scoped src="./index.css">

</style>