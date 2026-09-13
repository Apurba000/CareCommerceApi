workspace "CareCommerce" "Clinics discover, order and track medical supplies, and schedule the appointments around them." {

    !docs .
    !identifiers hierarchical

    model {
        practiceManager = person "Practice Manager" "Owns the clinic account and budget. Approves requisitions into orders and sets spend limits."
        clinicStaff     = person "Clinic Staff"     "Nurse, receptionist or assistant. Raises requisitions, books delivery and service slots, receives stock."
        supplierRep     = person "Supplier Rep"     "Lists and prices products, confirms delivery and service slots, updates fulfilment status."
        platformOps     = person "CareCommerce Ops" "Onboards clinics, resolves disputes, reviews the audit trail."

        careCommerce = softwareSystem "CareCommerce" "Discovery, ordering and tracking of clinical consumables and small equipment for outpatient clinics, plus the supply-side appointments they require. No patients, no PHI."

        idp         = softwareSystem "Identity Provider"          "Authenticates users and issues tokens. Entra External ID or Auth0." "External"
        messaging   = softwareSystem "Email and SMS Provider"     "Delivers order and appointment notifications. SendGrid or Twilio." "External"
        payments    = softwareSystem "Payment Gateway"            "Authorises and captures card payments. Stripe."                    "External"
        supplierErp = softwareSystem "Supplier Fulfilment System" "Receives purchase orders and returns shipment status."             "External"
        finance     = softwareSystem "Clinic Finance System"      "Consumes invoices for accounts payable."                           "External"

        practiceManager -> careCommerce "Places supply orders and approves spend using"    "HTTPS"
        clinicStaff     -> careCommerce "Schedules appointments and confirms deliveries in" "HTTPS"
        supplierRep     -> careCommerce "Publishes products and updates order status in"    "HTTPS"
        platformOps     -> careCommerce "Administers clinics and reviews audit logs in"     "HTTPS"

        careCommerce -> idp         "Delegates authentication to"                      "OIDC/HTTPS"
        careCommerce -> messaging   "Sends order and appointment notifications via"    "HTTPS/JSON"
        careCommerce -> payments    "Requests payment authorisation from"              "HTTPS/JSON"
        careCommerce -> supplierErp "Submits purchase orders to and polls status from" "HTTPS/JSON"
        careCommerce -> finance     "Publishes invoices to"                            "HTTPS/JSON"
    }

    views {
        systemContext careCommerce "SystemContext" {
            include *
            autolayout lr
            description "CareCommerce system context. Level 1 of 4. Owner: Apurba. Week 1."
        }

        styles {
            element "Person" {
                shape Person
                background "#08427b"
                color "#ffffff"
            }
            element "Software System" {
                background "#1168bd"
                color "#ffffff"
            }
            element "External" {
                background "#999999"
                color "#ffffff"
            }
        }

        theme default
    }
}
