/* ocLazyLoad config */

altairApp
    .config([
        '$ocLazyLoadProvider',
        function ($ocLazyLoadProvider) {
            $ocLazyLoadProvider.config({
                debug: false,
                events: false,
                modules: [
                    // ----------- UIKIT ------------------
                    {
                        name: 'lazy_uikit',
                        files: [
                            // uikit core
                            '/lib/uikit/js/uikit.min.js',
                            // uikit components
                            '/lib/uikit/js/components/accordion.min.js',
                            '/lib/uikit/js/components/autocomplete.min.js',
                            '/assets/js/custom/uikit_datepicker.min.js',
                            '/lib/uikit/js/components/form-password.min.js',
                            '/lib/uikit/js/components/form-select.min.js',
                            '/lib/uikit/js/components/grid.min.js',
                            '/lib/uikit/js/components/lightbox.min.js',
                            '/lib/uikit/js/components/nestable.min.js',
                            '/lib/uikit/js/components/notify.min.js',
                            '/lib/uikit/js/components/slider.min.js',
                            '/lib/uikit/js/components/slideshow.min.js',
                            '/lib/uikit/js/components/sortable.min.js',
                            '/lib/uikit/js/components/sticky.min.js',
                            '/lib/uikit/js/components/tooltip.min.js',
                            '/assets/js/custom/uikit_timepicker.min.js',
                            '/lib/uikit/js/components/upload.min.js',
                            '/assets/js/custom/uikit_beforeready.min.js',
                            // styles
                            '/assets/css/uikit.min.css'
                        ],
                        insertBefore: '#main_stylesheet',
                        serie: true
                    },

                    // ----------- FORM ELEMENTS -----------
                    {
                        name: 'lazy_autosize',
                        files: [
                            '/lib/autosize/dist/autosize.min.js',
                            '/app/modules/angular-autosize.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_iCheck',
                        files: [
                            '/lib/iCheck/icheck.min.js',
                            '/app/modules/angular-icheck.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_selectizeJS',
                        files: [
                            '/lib/selectize/dist/js/standalone/selectize.min.js',
                            '/app/modules/angular-selectize.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_switchery',
                        files: [
                            '/lib/switchery/dist/switchery.min.js',
                            '/app/modules/angular-switchery.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_ionRangeSlider',
                        files: [
                            '/lib/ion.rangeslider/js/ion.rangeSlider.min.js',
                            '/app/modules/angular-ionRangeSlider.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_masked_inputs',
                        files: [
                            '/lib/jquery.inputmask/dist/min/jquery.inputmask.bundle.min.js'
                        ]
                    },
                    {
                        name: 'lazy_character_counter',
                        files: [
                            '/app/modules/angular-character-counter.min.js'
                        ]
                    },
                    {
                        name: 'lazy_parsleyjs',
                        files: [
                            'assets/js/custom/parsleyjs_config.min.js',
                            '/lib/parsleyjs/dist/parsley.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_wizard',
                        files: [
                            '/lib/angular-wizard/dist/angular-wizard.min.js'
                        ]
                    },
                    {
                        name: 'lazy_ckeditor',
                        files: [
                            '/lib/ckeditor/ckeditor.js',
                            '/app/modules/angular-ckeditor.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_tinymce',
                        files: [
                            '/lib/tinymce/tinymce.min.js',
                            '/app/modules/angular-tinymce.min.js'
                        ],
                        serie: true
                    },

                    // ----------- CHARTS -----------
                    {
                        name: 'lazy_charts_chartist',
                        files: [
                            '/lib/chartist/dist/chartist.min.css',
                            '/lib/chartist/dist/chartist.min.js',
                            '/app/modules/angular-chartist.min.js'
                        ],
                        insertBefore: '#main_stylesheet',
                        serie: true
                    },
                    {
                        name: 'lazy_charts_easypiechart',
                        files: [
                            '/lib/jquery.easy-pie-chart/dist/angular.easypiechart.min.js'
                        ]
                    },
                    {
                        name: 'lazy_charts_metricsgraphics',
                        files: [
                            '/lib/metrics-graphics/dist/metricsgraphics.css',
                            '/lib/d3/d3.min.js',
                            '/lib/metrics-graphics/dist/metricsgraphics.min.js',
                            '/app/modules/angular-metrics-graphics.min.js'
                        ],
                        insertBefore: '#main_stylesheet',
                        serie: true
                    },
                    {
                        name: 'lazy_charts_c3',
                        files: [
                            '/lib/c3js-chart/c3.min.css',
                            '/lib/d3/d3.min.js',
                            '/lib/c3js-chart/c3.min.js',
                            '/lib/c3-angular/c3-angular.min.js'
                        ],
                        insertBefore: '#main_stylesheet',
                        serie: true
                    },
                    {
                        name: 'lazy_charts_peity',
                        files: [
                            '/lib/peity/jquery.peity.min.js',
                            '/app/modules/angular-peity.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_echarts',
                        files: [
                            '/lib/echarts/dist/echarts.js',
                            'assets/js/custom/echarts/maps/china.js',
                            '/app/modules/angular-echarts.js'
                        ],
                        serie: true
                    },

                    // ----------- COMPONENTS -----------
                    {
                        name: 'lazy_countUp',
                        files: [
                            '/lib/countUp.js/dist/countUp.min.js',
                            '/app/modules/angular-countUp.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_clndr',
                        files: [
                            '/lib/clndr/clndr.min.js',
                            '/lib/angular-clndr/angular-clndr.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_google_maps',
                        files: [
                            '/lib/ngmap/build/scripts/ng-map.min.js',
                            '/lib/ngmap/directives/places-auto-complete.js',
                            '/lib/ngmap/directives/marker.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_weathericons',
                        files: [
                            '/lib/weather-icons/css/weather-icons.min.css'
                        ],
                        insertBefore: '#main_stylesheet',
                        serie: true
                    },
                    {
                        name: 'lazy_prismJS',
                        files: [
                            '/lib/prism/prism.min.js',
                            '/lib/prism/components/prism-php.min.js',
                            '/lib/prism/plugins/line-numbers/prism-line-numbers.min.js',
                            '/app/modules/angular-prism.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_dragula',
                        files: [
                            '/lib/angular-dragula/dist/angular-dragula.min.js'
                        ]
                    },
                    {
                        name: 'lazy_pagination',
                        files: [
                            '/lib/angularUtils-pagination/dirPagination.js'
                        ]
                    },
                    {
                        name: 'lazy_diff',
                        files: [
                            '/lib/jsdiff/diff.min.js'
                        ]
                    },

                    // ----------- PLUGINS -----------
                    {
                        name: 'lazy_fullcalendar',
                        files: [
                            '/lib/fullcalendar/dist/fullcalendar.min.css',
                            '/lib/fullcalendar/dist/fullcalendar.min.js',
                            '/lib/fullcalendar/dist/gcal.js',
                            '/lib/angular-ui-calendar/src/calendar.min.js'
                        ],
                        insertBefore: '#main_stylesheet',
                        serie: true
                    },
                    {
                        name: 'lazy_codemirror',
                        files: [
                            '/lib/codemirror/lib/codemirror.css',
                            '/assets/css/codemirror_themes.min.css',
                            '/lib/codemirror/lib/codemirror.js',
                            '/assets/js/custom/codemirror_fullscreen.min.js',
                            '/lib/codemirror/addon/edit/matchtags.js',
                            '/lib/codemirror/addon/edit/matchbrackets.js',
                            '/lib/codemirror/addon/fold/xml-fold.js',
                            '/lib/codemirror/mode/htmlmixed/htmlmixed.js',
                            '/lib/codemirror/mode/xml/xml.js',
                            '/lib/codemirror/mode/php/php.js',
                            '/lib/codemirror/mode/clike/clike.js',
                            '/lib/codemirror/mode/javascript/javascript.js',
                            'app/modules/angular-codemirror.min.js'
                        ],
                        insertBefore: '#main_stylesheet',
                        serie: true
                    },
                    {
                        name: 'lazy_datatables',
                        files: [
                            '/lib/datatables/media/js/jquery.dataTables.min.js',
                            '/lib/angular-datatables/dist/angular-datatables.js',
                            'assets/js/custom/jquery.dataTables.columnFilter.js',
                            '/lib/angular-datatables/dist/plugins/columnfilter/angular-datatables.columnfilter.min.js',
                            'assets/js/custom/datatables/datatables.uikit.js',
                            // buttons
                            '/lib/datatables-buttons/js/dataTables.buttons.js',
                            '/lib/angular-datatables/dist/plugins/buttons/angular-datatables.buttons.min.js',
                            'assets/js/custom/datatables/buttons.uikit.js',
                            '/lib/jszip/dist/jszip.min.js',
                            '/lib/pdfmake/build/pdfmake.min.js',
                            '/lib/pdfmake/build/vfs_fonts.js',
                            '/lib/datatables-buttons/js/buttons.colVis.js',
                            '/lib/datatables-buttons/js/buttons.html5.js',
                            '/lib/datatables-buttons/js/buttons.print.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_gantt_chart',
                        files: [
                            '/lib/jquery-ui/jquery-ui.min.js',
                            'assets/js/custom/gantt_chart.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_tablesorter',
                        files: [
                            '/lib/tablesorter/dist/js/jquery.tablesorter.min.js',
                            '/lib/tablesorter/dist/js/jquery.tablesorter.widgets.min.js',
                            '/lib/tablesorter/dist/js/widgets/widget-alignChar.min.js',
                            '/lib/tablesorter/dist/js/widgets/widget-columnSelector.min.js',
                            '/lib/tablesorter/dist/js/widgets/widget-print.min.js',
                            '/lib/tablesorter/dist/js/extras/jquery.tablesorter.pager.min.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_vector_maps',
                        files: [
                            '/lib/raphael/raphael.min.js',
                            '/lib/jquery-mapael/js/jquery.mapael.js',
                            '/lib/jquery-mapael/js/maps/world_countries.js',
                            '/lib/jquery-mapael/js/maps/usa_states.js'
                        ],
                        serie: true
                    },
                    {
                        name: 'lazy_dropify',
                        files: [
                            'assets/skins/dropify/css/dropify.css',
                            '/lib/dropify/dist/js/dropify.min.js'
                        ],
                        insertBefore: '#main_stylesheet'
                    },
                    {
                        name: 'lazy_tree',
                        files: [
                            'assets/skins/jquery.fancytree/ui.fancytree.min.css',
                            '/lib/jquery-ui/jquery-ui.min.js',
                            '/lib/jquery.fancytree/dist/jquery.fancytree-all.min.js'
                        ],
                        insertBefore: '#main_stylesheet',
                        serie: true
                    },
                    {
                        name: 'lazy_idle_timeout',
                        files: [
                            '/lib/ng-idle/angular-idle.min.js'
                        ]
                    },
                    {
                        name: 'lazy_tour',
                        files: [
                            '/lib/enjoyhint/enjoyhint.min.js'
                        ]
                    },
                    {
                        name: 'lazy_filemanager',
                        files: [
                            '/lib/jquery-ui/themes/smoothness/jquery-ui.min.css',
                            'file_manager/css/elfinder.min.css',
                            'file_manager/themes/material/css/theme.css',
                            '/lib/jquery-ui/jquery-ui.min.js',
                            'file_manager/js/elfinder.min.js',
                            'file_manager/js/i18n/elfinder.ar.js'
                        ],
                        insertBefore: '#main_stylesheet',
                        serie: true
                    },
                    {
                        name: 'lazy_image_cropper',
                        files: [
                            '/lib/cropper/dist/cropper.min.css',
                            '/lib/cropper/dist/cropper.min.js'
                        ]
                    },
                    {
                        name: 'lazy_context_menu',
                        files: [
                            '/lib/jQuery-contextMenu/dist/jquery.ui.position.min.js',
                            '/lib/jQuery-contextMenu/dist/jquery.contextMenu.min.css',
                            '/lib/jQuery-contextMenu/dist/jquery.contextMenu.min.js'
                        ],
                        insertBefore: '#main_stylesheet',
                        serie: true
                    },
                    {
                        name: 'lazy_quiz',
                        files: [
                            'assets/js/custom/slickQuiz/slickQuiz.js'
                        ]
                    },
                    {
                        name: 'lazy_listNav',
                        files: [
                            '/lib/jquery-listnav/jquery-listnav.min.js'
                        ]
                    },
                    {
                        name: 'lazy_uiSelect',
                        files: [
                            '/lib/angular-ui-select/dist/select.min.css',
                            '/lib/angular-ui-select/dist/select.min.js'
                        ]
                    },

                    // ----------- KENDOUI COMPONENTS -----------
                    {
                        name: 'lazy_KendoUI',
                        files: [
                            '/lib/kendo-ui/js/kendo.core.min.js',
                            '/lib/kendo-ui/styles/kendo.rtl.min.css',
                            '/lib/kendo-ui/js/kendo.color.min.js',
                            '/lib/kendo-ui/js/kendo.data.min.js',
                            '/lib/kendo-ui/js/kendo.calendar.min.js',
                            '/lib/kendo-ui/js/kendo.popup.min.js',
                            '/lib/kendo-ui/js/kendo.datepicker.min.js',
                            '/lib/kendo-ui/js/kendo.timepicker.min.js',
                            '/lib/kendo-ui/js/kendo.datetimepicker.min.js',
                            '/lib/kendo-ui/js/kendo.list.min.js',
                            '/lib/kendo-ui/js/kendo.fx.min.js',
                            '/lib/kendo-ui/js/kendo.userevents.min.js',
                            '/lib/kendo-ui/js/kendo.menu.min.js',
                            '/lib/kendo-ui/js/kendo.draganddrop.min.js',
                            '/lib/kendo-ui/js/kendo.slider.min.js',
                            '/lib/kendo-ui/js/kendo.mobile.scroller.min.js',
                            '/lib/kendo-ui/js/kendo.autocomplete.min.js',
                            '/lib/kendo-ui/js/kendo.combobox.min.js',
                            '/lib/kendo-ui/js/kendo.dropdownlist.min.js',
                            '/lib/kendo-ui/js/kendo.colorpicker.min.js',
                            '/lib/kendo-ui/js/kendo.combobox.min.js',
                            '/lib/kendo-ui/js/kendo.maskedtextbox.min.js',
                            '/lib/kendo-ui/js/kendo.multiselect.min.js',
                            '/lib/kendo-ui/js/kendo.numerictextbox.min.js',
                            '/lib/kendo-ui/js/kendo.toolbar.min.js',
                            '/lib/kendo-ui/js/kendo.panelbar.min.js',
                            '/lib/kendo-ui/js/kendo.window.min.js',
                            '/lib/kendo-ui/js/kendo.angular.min.js',
                            '/lib/kendo-ui/styles/kendo.common-material.min.css',
                            '/lib/kendo-ui/styles/kendo.material.min.css'
                        ],
                        insertBefore: '#main_stylesheet',
                        serie: true
                    },

                    // ----------- UIKIT HTMLEDITOR -----------
                    {
                        name: 'lazy_htmleditor',
                        files: [
                            '/lib/codemirror/lib/codemirror.js',
                            '/lib/codemirror/mode/markdown/markdown.js',
                            '/lib/codemirror/addon/mode/overlay.js',
                            '/lib/codemirror/mode/javascript/javascript.js',
                            '/lib/codemirror/mode/php/php.js',
                            '/lib/codemirror/mode/gfm/gfm.js',
                            '/lib/codemirror/mode/xml/xml.js',
                            '/lib/marked/lib/marked.js',
                            '/lib/uikit/js/components/htmleditor.js'
                        ],
                        serie: true
                    },

                    // ----------- THEMES -------------------
                    {
                        name: 'lazy_themes',
                        files: [
                            '/assets/css/themes/_theme_a.min.css',
                            '/assets/css/themes/_theme_b.min.css',
                            '/assets/css/themes/_theme_c.min.css',
                            '/assets/css/themes/_theme_d.min.css',
                            '/assets/css/themes/_theme_e.min.css',
                            '/assets/css/themes/_theme_f.min.css',
                            '/assets/css/themes/_theme_g.min.css',
                            '/assets/css/themes/_theme_h.min.css',
                            '/assets/css/themes/_theme_i.min.css',
                            '/assets/css/themes/_theme_dark.min.css'
                        ]
                    },
                    {
                        name: 'ADM',
                        files: [
                            '/lib/ADM-dateTimePicker/ADM-dateTimePicker.min.css',
                            '/lib/ADM-dateTimePicker/ADM-dateTimePicker.min.js'
                        ]
                    },
                    //{
                    //    name: 'momentjs',
                    //    files: [
                    //        '/lib/moment/moment.js',      >>> momentjs
                    //        '/lib/moment/angular-moment.min.js'
                    //    ]
                    //},
                    {
                        name: 'angucomplete-alt',
                        files: [
                            '/lib/angucomplete-alt/angucomplete-alt.min.css',
                            '/lib/angucomplete-alt/angucomplete-alt.min.js'
                        ]
                    },
                    {
                        name: 'primitive-chart',
                        files: [
                            '/lib/jquery-ui/themes/smoothness/jquery-ui.min.css',
                            '/lib/primitives/primitives.latest.css',
                            '/lib/jquery-ui/jquery-ui.min.js',
                            '/lib/primitives/primitives.min.js',
                        ],
                        serie: true
                    }
                ]
            })
        }
    ]);